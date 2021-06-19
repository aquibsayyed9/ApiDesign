using AutoMapper;
using EzPay.DataAccess.Interfaces;
using EzPay.DTO;
using EzPay.Entities;
using EzPay.Repository.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzPay.DataAccess.Classes
{
    public class PaymentDataAccess : IPaymentDataAccess
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        private IMapper _mapper;
        IPaymentTransactionsRepository _paymentTransRepository;
        IPaymentGatewayRepository _paymentGatewayRepository;
        public PaymentDataAccess(IPaymentTransactionsRepository paymentTransRepository, IPaymentGatewayRepository paymentGatewayRepository, IMapper mapper)
        {
            _paymentGatewayRepository = paymentGatewayRepository;
            _paymentTransRepository = paymentTransRepository;
            _mapper = mapper;
        }

        public bool UpdatePT(PaymentTransactions payment)
        {
            try
            {
                _paymentTransRepository.Edit(payment);
                _paymentTransRepository.Save();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        public PaymentGatewayDTO GetPaymentGatewayDetails(PaymentGatewayDTO paymentGatewayDTO)
        {
            PaymentGatewayDTO returnObject = null;
            try
            {
                PaymentGatewayMaster paymentGatewayMaster = null;
                if (paymentGatewayDTO.CompanyID > 0 && paymentGatewayDTO.ChargeTypeID > 0 && paymentGatewayDTO.TowerID > 0)
                {
                    logger.Info("Company ID, ChargeType ID & Tower ID is not null");
                    paymentGatewayMaster = _paymentGatewayRepository.FindBy(a => a.Active == true && a.CompanyID == paymentGatewayDTO.CompanyID
                    && a.ChargeTypeID == paymentGatewayDTO.ChargeTypeID && a.TowerID == paymentGatewayDTO.TowerID).FirstOrDefault();
                }
                else if (paymentGatewayDTO.CompanyID > 0 && paymentGatewayDTO.ChargeTypeID > 0)
                {
                    logger.Info("Company ID, ChargeType ID is not null");
                    paymentGatewayMaster = _paymentGatewayRepository.FindBy(a => a.Active == true && a.CompanyID == paymentGatewayDTO.CompanyID
                    && a.ChargeTypeID == paymentGatewayDTO.ChargeTypeID).FirstOrDefault();
                }
                else if (paymentGatewayDTO.CompanyID > 0)
                {
                    logger.Info("Company ID is not null");
                    paymentGatewayMaster = _paymentGatewayRepository.FindBy(a => a.Active == true && a.CompanyID == paymentGatewayDTO.CompanyID).FirstOrDefault();
                }
                else
                {
                    logger.Info("Company ID, ChargeType ID & Tower ID are all null or not active");
                    return returnObject;
                }

                logger.Info("Got Details, Mapping Entity to DTO and Exiting Method");
                returnObject = _mapper.Map<PaymentGatewayMaster, PaymentGatewayDTO>(paymentGatewayMaster);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return returnObject;
        }

        public List<PaymentTransactionsDTO> GetPaymentTransaction(PaymentTransactionsDTO payTransaction)
        {
            List<PaymentTransactionsDTO> lstPaymentTransactionsDTO = null;
            try
            {
                logger.Info("Mapping PaymentTransactionsDTO to Entity");
                PaymentTransactions transactionEntity = _mapper.Map<PaymentTransactionsDTO, PaymentTransactions>(payTransaction);

                logger.Info("Mapping completed, getting Transaction Details");
                var results = _paymentTransRepository.GetList(transactionEntity);

                logger.Info("Got Transaction Details, Mapping Entity to DTO and Exiting Method");
                lstPaymentTransactionsDTO = _mapper.Map<List<PaymentTransactions>, List<PaymentTransactionsDTO>>(results.ToList<PaymentTransactions>());
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return lstPaymentTransactionsDTO;
        }

        public PaymentTransactionsDTO SavePaymentTransaction(PaymentTransactionsDTO payTransaction)
        {
            try
            {
                logger.Info("Mapping DTO to Entity & Saving Payment Transaction In DB");
                PaymentTransactions transaction = _mapper.Map<PaymentTransactionsDTO, PaymentTransactions>(payTransaction);
                _paymentTransRepository.Add(transaction);
                _paymentTransRepository.Save();

                logger.Info($"Transaction save in DB with ID: {transaction.ID}, Mapping Entity to DTO and exiting method");
                PaymentTransactionsDTO transactionsDTO = _mapper.Map<PaymentTransactions, PaymentTransactionsDTO>(transaction);
                return transactionsDTO;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public bool UpdatePaymentTransactionDetails(PaymentTransactionsDTO paymentDTO, int flag)
        {
            try
            {
                logger.Info($"Creating PaymentTransactionDetail Entity object from paymentDTO with Id: {paymentDTO.ID}, Flag Value: {flag} and getting data from PTD");

                //PaymentTransactions paymentTransaction = _paymentTransRepository.Get(new PaymentTransactions
                //{
                //    ID = paymentDTO.ID
                //});
                PaymentTransactions paymentTransaction = new PaymentTransactions() { TransactionID = paymentDTO.TransactionID };

                if (paymentTransaction != null)
                {
                    logger.Info("Got PTD item, checking PTDUpdate Enum");
                    if (flag == 1)
                    {
                        logger.Info("Setting values to be updated for Com Trust Transaction");
                        paymentTransaction.ApprovalCode = paymentDTO.ApprovalCode;
                        paymentTransaction.LastModifiedDt = DateTime.UtcNow;
                        paymentTransaction.OrderID = paymentDTO.OrderID;
                        paymentTransaction.GatewayStatus = paymentDTO.GatewayStatus;
                        paymentTransaction.ResponseCode = paymentDTO.ResponseCode;
                        paymentTransaction.ResponseDescription = paymentDTO.ResponseDescription;
                        paymentTransaction.TransactionID = paymentDTO.TransactionID;
                    }
                    else if (flag == 2)
                    {
                        logger.Info("Setting values to be updated for UpdateOrderID");
                        paymentTransaction.OrderID = paymentDTO.OrderID;
                    }

                    logger.Info("Properties have been set, calling Method: UpdatePT to update PT Table");
                    return UpdatePT(paymentTransaction);
                }
                else
                {
                    logger.Info("No items found in PT Table, exiting method");
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        public bool UpdateGatewayStatus(string transactionId, string status)
        {
            try
            {
                logger.Info($"Creating PaymentTransactionDetail Entity object from paymentDTO with transaction Id: {transactionId}, for updating payment status");

                PaymentTransactions paymentTransaction = _paymentTransRepository.FindBy(x => x.TransactionID == transactionId).FirstOrDefault();

                if (paymentTransaction != null)
                {
                    logger.Info("Got PTD item, setting status");

                    paymentTransaction.GatewayStatus = status;

                    logger.Info("Properties have been set, calling Method: UpdatePT to update PT Table");
                    return UpdatePT(paymentTransaction);
                }
                else
                {
                    logger.Info("No items found in PT Table, exiting method");
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        public PaymentGatewayDTO GetSinglePG(PaymentGatewayDTO paymentGatewayDTO)
        {
            PaymentGatewayMaster pgMaster = _mapper.Map<PaymentGatewayMaster>(paymentGatewayDTO);
            var result = _paymentGatewayRepository.GetList(pgMaster).FirstOrDefault();
            PaymentGatewayDTO dto = _mapper.Map<PaymentGatewayDTO>(result);
            return dto;
        }
    }
}
