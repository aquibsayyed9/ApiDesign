using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DataAccess.Interfaces
{
    public interface IPaymentDataAccess
    {
        PaymentGatewayDTO GetPaymentGatewayDetails(PaymentGatewayDTO paymentGatewayDTO);
        List<PaymentTransactionsDTO> GetPaymentTransaction(PaymentTransactionsDTO payTransaction);
        PaymentTransactionsDTO SavePaymentTransaction(PaymentTransactionsDTO payTransaction);
        bool UpdatePaymentTransactionDetails(PaymentTransactionsDTO paymentDTO, int flag);
        bool UpdateGatewayStatus(string transactionId, string status);
        PaymentGatewayDTO GetSinglePG(PaymentGatewayDTO paymentGatewayDTO);
    }
}
