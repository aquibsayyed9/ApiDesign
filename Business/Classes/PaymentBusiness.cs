using EzPay.Business.Interfaces;
using EzPay.DataAccess.Interfaces;
using EzPay.DTO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzPay.Business.Classes
{
    public class PaymentBusiness : IPaymentBusiness
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        IPaymentDataAccess _paymentDataAccess;
        IComTrustBusiness _comTrustBusiness;
        ICyberSource _cyberSource;
        IEmailBusines _emailBusiness;
        private readonly AppSettings _appSettings;
        public PaymentBusiness(IPaymentDataAccess paymentDataAccess, IComTrustBusiness comTrustBusiness, ICyberSource cyberSource, IOptions<AppSettings> appSettings
            , IEmailBusines emailBusiness)
        {
            _paymentDataAccess = paymentDataAccess;
            _comTrustBusiness = comTrustBusiness;
            _cyberSource = cyberSource;
            _appSettings = appSettings.Value;
            _emailBusiness = emailBusiness;
        }

        //#TODO:
        //Replace flag with Enum
        public PaymentGatewayDTO GetPaymentGatewayDetails(PaymentGatewayDTO paymentGatewayDTO, int flag)
        {
            return _paymentDataAccess.GetPaymentGatewayDetails(paymentGatewayDTO);
        }
        public List<PaymentTransactionsDTO> GetPaymentTransaction(PaymentTransactionsDTO transaction)
        {
            return _paymentDataAccess.GetPaymentTransaction(transaction);
        }
        ComTrustResponse GetComTrustResponse(PaymentTransactionsDTO transactionsDTO, PaymentGatewayDTO paymentGatewayDTO)
        {
            try
            {
                logger.Info("Calling Method: GenerateComTrustModel");
                ComTrustRegistrationDTO comTrustTransactionDTO = _comTrustBusiness.GenerateComTrustModel(transactionsDTO, paymentGatewayDTO);
                if (comTrustTransactionDTO != null)
                {
                    logger.Info("Successfully Generated Com Trust Model, calling method: GetComTrustTransactionDetails to get transaction id from Com Trust");
                    ComTrustResponse comTrustResponse = _comTrustBusiness.GetComTrustTransactionDetails(comTrustTransactionDTO);
                    return comTrustResponse;
                }

                logger.Info("Unable to generated Com Trust Model");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }
        CyberSourceDTO GetCyberSourceResponse(PaymentTransactionsDTO paymentTransactionsDTO, PaymentGatewayDTO paymentGatewayDTO)
        {
            try
            {
                bool status = false;
                string amount = Convert.ToDouble(paymentTransactionsDTO.PaidAmount).ToString("#.##");
                CyberSourceDTO cyberSourceDTO = _cyberSource.GenerateCSModel(paymentTransactionsDTO, amount);

                if (cyberSourceDTO != null)
                {
                    logger.Info("Cyber Source DTO object generated successfully, Calling Method: GetSignedData to signed data");
                    cyberSourceDTO.access_key = paymentGatewayDTO.AccessKey;
                    cyberSourceDTO.PGType = paymentGatewayDTO.PGType;
                    cyberSourceDTO.profile_id = paymentGatewayDTO.ProfileID;
                    cyberSourceDTO.SecretKey = paymentGatewayDTO.SecretKey;

                    cyberSourceDTO = _cyberSource.GetSignedData(cyberSourceDTO, out status);
                    return cyberSourceDTO;
                }

                logger.Info("Unable to generated Cyber Source Model");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        //#TODO:        
        public object RedirectToPaymentGateway(PaymentTransactionsDTO paymentTransactionsDTO)
        {
            string message = "Sorry, Something went wrong";
            bool status = false;
            try
            {
                logger.Info("Log the data by serialzing json");


                logger.Info($"Serialized and logged data, Creating Payment Gateway DTO object using Company ID: {paymentTransactionsDTO.CompanyID}, Charge Type ID: {paymentTransactionsDTO.ChargeTypeID}, Tower ID: {paymentTransactionsDTO.TowerID}");
                PaymentGatewayDTO paymentGatewayDTO = new PaymentGatewayDTO
                {
                    CompanyID = paymentTransactionsDTO.CompanyID,
                    ChargeTypeID = paymentTransactionsDTO.ChargeTypeID,
                    TowerID = paymentTransactionsDTO.TowerID
                };

                logger.Info("Successfully created Payment Gateway DTO object, Calling Method: GetPaymentGatewayDetails");
                PaymentGatewayDTO dbPaymentGatewayDTO = GetPaymentGatewayDetails(paymentGatewayDTO, 0);
                if (dbPaymentGatewayDTO != null)
                {
                    logger.Info($"dbPaymentGatewayDTO is not null, updating properties of paymentTransactionsDTO");
                    paymentTransactionsDTO.CreatedDate = DateTime.UtcNow;
                    paymentTransactionsDTO.GatewayStatus = "Pending";
                    paymentTransactionsDTO.LastModifiedDt = DateTime.UtcNow;
                    paymentTransactionsDTO.OrderID = Guid.NewGuid().ToString().Substring(0, 16);//Guid.NewGuid().ToString();
                    paymentTransactionsDTO.PGType = dbPaymentGatewayDTO.PGType;

                    logger.Info("Properties have been updated, Switching on PG Type");
                    //#TODO: Use PGType Enum
                    switch (paymentTransactionsDTO.PGType)
                    {
                        case "ComTrust":
                            paymentTransactionsDTO.PGDetails = $"{dbPaymentGatewayDTO.Store}:{dbPaymentGatewayDTO.Terminal}";
                            logger.Info("PG Type is Com Trust, Calling Method: GetComTrustResponse");
                            ComTrustResponse comTrustResponse = GetComTrustResponse(paymentTransactionsDTO, dbPaymentGatewayDTO);

                            if (comTrustResponse != null)
                            {
                                message = $"Successfully Generated Com Trust Transaction Data with Transaction ID: {comTrustResponse.TransactionID}";
                                logger.Info($"{message}, Updating paymentTransactionsDTO properties");
                                paymentTransactionsDTO.TransactionID = comTrustResponse.TransactionID;
                                paymentTransactionsDTO.ApprovalCode = comTrustResponse.ResponseClass;
                                paymentTransactionsDTO.ResponseCode = comTrustResponse.ResponseCode;
                                paymentTransactionsDTO.ResponseDescription = comTrustResponse.ResponseDescription;

                                logger.Info($"Updated Properties, Calling Method: SavePaymentTransaction to Save Payment Transaction for Customer Name: {paymentTransactionsDTO.Name} with Email ID: {paymentTransactionsDTO.EmailID}");
                                PaymentTransactionsDTO returnObject = _paymentDataAccess.SavePaymentTransaction(paymentTransactionsDTO);
                                if (returnObject != null && returnObject.ID > 0)
                                {
                                    logger.Info($"Successful saved payment transaction with ID: {returnObject.ID}");
                                    status = true;
                                    CustomResultDTO<ComTrustResponse> transactionResult = new CustomResultDTO<ComTrustResponse>
                                    {
                                        Message = message,
                                        Param = new ObjectParamsDTO() { Value = "ComTrust" },
                                        Result = comTrustResponse,
                                        Status = status
                                    };
                                    return transactionResult;
                                }
                                else
                                {
                                    message = "Unable to save Payment Transaction in DB";
                                    logger.Info(message);
                                }
                            }
                            else
                            {
                                logger.Info("Unable to generate Com Trust Object");
                            }
                            break;
                        case "CyberSource":
                            paymentTransactionsDTO.PGDetails = $"{dbPaymentGatewayDTO.MerchantID}:{dbPaymentGatewayDTO.ProfileID}";
                            logger.Info("PG type is Cyber Source, Calling Method: GetCyberSourceResponse");
                            CyberSourceDTO cyberSourceDTO = GetCyberSourceResponse(paymentTransactionsDTO, dbPaymentGatewayDTO);

                            if (cyberSourceDTO != null)
                            {
                                message = $"Successfully Generated Signed Cyber Source Data";
                                logger.Info($"{message}, Calling Method: SavePaymentTransaction to Save Payment Transaction for Customer Name: {paymentTransactionsDTO.Name} with Email ID: {paymentTransactionsDTO.EmailID}");
                                PaymentTransactionsDTO returnObject = _paymentDataAccess.SavePaymentTransaction(paymentTransactionsDTO);
                                if (returnObject != null && returnObject.ID > 0)
                                {
                                    logger.Info($"Successful saved payment transaction with ID: {returnObject.ID}");
                                    status = true;
                                    var transactionData = new CustomResultDTO<dynamic>()
                                    {
                                        Message = message,
                                        Param = new ObjectParamsDTO() { Value = "CyberSource" },
                                        Result = cyberSourceDTO,
                                        Status = status
                                    };
                                    return transactionData;
                                }
                                else
                                {
                                    message = "Unable to save Payment Transaction in DB";
                                    logger.Info(message);
                                }
                            }
                            else
                            {
                                logger.Info("Unable to generate Cyber Source object");
                            }
                            break;
                        default:
                            logger.Info("Incorrect PG Type");
                            break;
                    }
                }
                else
                {
                    logger.Info("DB Payment Gateway DTO is null");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            logger.Info("Unable to generate PG type, returning failure");
            CustomResultDTO<dynamic> returnValue = new CustomResultDTO<dynamic>
            {
                Message = message,
                Param = null,
                Result = null,
                Status = status
            };

            return returnValue;
        }

        public bool UpdatePaymentTransactionDetails(PaymentTransactionsDTO paymentDTO, int flag)
        {
            return _paymentDataAccess.UpdatePaymentTransactionDetails(paymentDTO, flag);
        }

        public bool UpdateGatewayStatus(string transactionId, string status)
        {
            return _paymentDataAccess.UpdateGatewayStatus(transactionId, status);
        }
          
        public ComTrustFinalizeResponse ComTrustFinalize(string transactionId)
        {
            PaymentTransactionsDTO txnObj = _paymentDataAccess.GetPaymentTransaction(new PaymentTransactionsDTO { TransactionID = transactionId }).FirstOrDefault();
            PaymentGatewayDTO pgObj = _paymentDataAccess.GetSinglePG(new PaymentGatewayDTO { PGType = txnObj.PGType });

            ComTrustFinalizeResponse comTrustFinalizeResponse = _comTrustBusiness.FinalizeComTrustPayment(
                new ComTrustFinalizeRequest
                {
                    Finalization = new ComTrustFinalization
                    {
                        Customer = pgObj.CustomerName,
                        Password = pgObj.Password,
                        UserName = pgObj.Username,
                        TransactionID = transactionId
                    }
                });

            return comTrustFinalizeResponse;
        }

        public void sendSuccessPaymentEmail(string transactionId, string emailBody)
        {
            if (!Boolean.Parse(_appSettings.IsEmailSendActive))
            {
                logger.Info("Email not to be sent as per config, returning control back to PaymentController");
                return;
            }
            
            PaymentTransactionsDTO txnObj = _paymentDataAccess.GetPaymentTransaction(new PaymentTransactionsDTO { TransactionID = transactionId }).FirstOrDefault();

            _emailBusiness.Send(_appSettings.FromEmail, txnObj.EmailID, _appSettings.SuccessEmailSub, getEmailBody(emailBody, txnObj), "", _appSettings.BccSuccessEmail);

            logger.Info("Email sent successfully");
        }

        public string getEmailBody(string emailBody, PaymentTransactionsDTO txnObj)
        {
            Unit unitDetails = JsonConvert.DeserializeObject<Unit>(txnObj.data.TrimStart('[').TrimEnd(']'));

            emailBody = emailBody.Replace("%customerName%", txnObj.Name);
            emailBody = emailBody.Replace("%dueAmount%", Convert.ToString(unitDetails.DueAmount));
            emailBody = emailBody.Replace("%paidAmount%", Convert.ToString(txnObj.PaidAmount));
            emailBody = emailBody.Replace("%txnDate%", Convert.ToDateTime(txnObj.CreatedDate).ToString("dd MMM yyyy"));

            return emailBody;
        }
    }
}
