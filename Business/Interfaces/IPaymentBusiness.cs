using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Business.Interfaces
{
    public interface IPaymentBusiness
    {
        PaymentGatewayDTO GetPaymentGatewayDetails(PaymentGatewayDTO paymentGatewayDTO, int flag);
        List<PaymentTransactionsDTO> GetPaymentTransaction(PaymentTransactionsDTO transaction);
        object RedirectToPaymentGateway(PaymentTransactionsDTO paymentTransactionsDTO);
        bool UpdatePaymentTransactionDetails(PaymentTransactionsDTO paymentDTO, int flag);
        bool UpdateGatewayStatus(string transactionId, string status);
        ComTrustFinalizeResponse ComTrustFinalize(string transactionId);
        void sendSuccessPaymentEmail(string transactionId, string emailBody);
    }
}
