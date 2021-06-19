using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Business.Interfaces
{
    public interface IComTrustBusiness
    {
        ComTrustResponse RegisterComTrustTransaction(ComTrustRegistrationDTO registerModel, long paymentId);
        ComTrustFinalizeResponse FinalizeComTrustPayment(ComTrustFinalizeRequest request);
        ComTrustRegistrationDTO GenerateComTrustModel(PaymentTransactionsDTO paymentDTO, PaymentGatewayDTO paymentGatewayDTO);
        ComTrustResponse GetComTrustTransactionDetails(ComTrustRegistrationDTO registerModel);
    }
}
