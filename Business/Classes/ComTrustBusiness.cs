using EzPay.Business.Interfaces;
using EzPay.DTO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace EzPay.Business.Classes
{
    public class ComTrustBusiness : IComTrustBusiness
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly AppSettings _appSettings;

        public ComTrustBusiness(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public ComTrustFinalizeResponse FinalizeComTrustPayment(ComTrustFinalizeRequest request)
        {
            try
            {
                logger.Info("Preparing To Finalize FAB Payment");
                using (var client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(request);
                    logger.Info("Request Serialized To Json ===>" + json);
                    StringContent strContent = new StringContent(json);
                    strContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    byte[] modelBytes = ASCIIEncoding.ASCII.GetBytes(json);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    client.DefaultRequestHeaders.Add("Accept", "text/xml-standard-api");
                    client.DefaultRequestHeaders.Add("UserAgent", ".NET Framework Test Client");
                    client.DefaultRequestHeaders.Add("ContentLength", modelBytes.Length.ToString());
                    client.DefaultRequestHeaders.Add("Timeout", "600000");
                    logger.Info(string.Format("initializing the FAB finalize process with transactionId-{0}", request.Finalization.TransactionID));
                    var response = client.PostAsync(_appSettings.ComTrustGateWayUrl, strContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = response.Content.ReadAsStringAsync().Result;
                        logger.Info(string.Format("finalize process for transactionId-{0} is success", request.Finalization.TransactionID));
                        return JsonConvert.DeserializeObject<ComTrustFinalizeResponse>(resultString);
                    }
                    else
                    {
                        logger.Info(string.Format("finalize process for transactionId-{0} is failed", request.Finalization.TransactionID));
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public ComTrustRegistrationDTO GenerateComTrustModel(PaymentTransactionsDTO paymentDTO, PaymentGatewayDTO paymentGatewayDTO)
        {
            ComTrustRegistrationDTO transactionDTO = null;
            try
            {
                logger.Info("Inside Method: GenerateComTrustModel, Creating Com Trust Registration");
                transactionDTO = new ComTrustRegistrationDTO();
                ComTrustRegistration fabRegistration = new ComTrustRegistration()
                {
                    Amount = paymentDTO.PaidAmount.ToString("#.##"),
                    OrderName = "PayBill",
                    OrderID = paymentDTO.OrderID,
                    ReturnPath = _appSettings.ComTrustReturnPath,
                    Channel = "Web",
                    Currency = "AED",
                    TransactionHint = "CPT:Y;VCC:Y;",
                    Customer = paymentGatewayDTO.CustomerName,
                    Password = paymentGatewayDTO.Password,
                    Store = paymentGatewayDTO.Store,
                    Terminal = paymentGatewayDTO.Terminal,
                    UserName = paymentGatewayDTO.Username
                };
                transactionDTO.Registration = fabRegistration;

                logger.Info("Com Trust Registration Created, exiting method");
                if (_appSettings.LogCT_PG_REQ == "1")
                {
                    logger.Info($"Com Trust Request JSON: {JsonConvert.SerializeObject(transactionDTO)}");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return transactionDTO;
        }

        public ComTrustResponse GetComTrustTransactionDetails(ComTrustRegistrationDTO registerModel)
        {
            ComTrustResponse transaction = null;
            try
            {
                ComTrustResponseDTO jsonResult = null;
                logger.Info("Preparing json data to call Com Trust Service");
                using (var client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(registerModel);

                    logger.Info("Request Serialized To Json, Preparing Content");
                    StringContent strContent = new StringContent(json);
                    strContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    byte[] modelBytes = ASCIIEncoding.ASCII.GetBytes(json);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    client.DefaultRequestHeaders.Add("Accept", "text/xml-standard-api");
                    client.DefaultRequestHeaders.Add("UserAgent", ".NET Framework Test Client");
                    client.DefaultRequestHeaders.Add("ContentLength", modelBytes.Length.ToString());
                    client.DefaultRequestHeaders.Add("Timeout", "600000");

                    logger.Info("Data Prepared, Calling Service");
                    var response = client.PostAsync(_appSettings.ComTrustGateWayUrl, strContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = response.Content.ReadAsStringAsync().Result;
                        logger.Info($"Com Trust Registration successfull with response: {resultString} {Environment.NewLine} De-Serializing to get Response");
                        jsonResult = JsonConvert.DeserializeObject<ComTrustResponseDTO>(resultString);
                        transaction = jsonResult.Transaction;
                    }
                    else
                    {
                        if (response.Content != null)
                        {
                            var resultString = response.Content.ReadAsStringAsync().Result;
                            logger.Info($"Com Trust Registration Failed with response code: {response.StatusCode} & response content: {resultString}");
                        }
                        else
                        {
                            logger.Info("Bad Request on estalishing connection to Com Trust Server");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return transaction;
        }

        public ComTrustResponse RegisterComTrustTransaction(ComTrustRegistrationDTO registerModel, long paymentId)
        {
            throw new NotImplementedException();
        }
    }
}
