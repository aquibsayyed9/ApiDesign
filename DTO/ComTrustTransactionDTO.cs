using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DTO
{
    public class ComTrustRegistrationDTO //RegistrationModel
    {
        public ComTrustRegistration Registration { get; set; }
    }

    public class ComTrustRegistration //FABRegistration
    {
        public string Currency { get; set; }
        public string ReturnPath { get; set; }
        public string TransactionHint { get; set; }
        public string OrderID { get; set; }
        public string Store { get; set; }
        public string Terminal { get; set; }
        public string Channel { get; set; }
        public string Amount { get; set; }
        public string Customer { get; set; }
        public string OrderName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class ComTrustBalance //Balance
    {
        public string Value { get; set; }
    }

    public class ComTrustAmount //Amount
    {
        public string Value { get; set; }
    }

    public class ComTrustFees //Fees
    {
        public string Value { get; set; }
    }

    public class ComTrustResponseDTO //FABTransactionModel
    {
        public ComTrustResponse Transaction { get; set; }
    }

    public class ComTrustResponse //FABTransaction
    {
        public string PaymentPortal { get; set; }
        public string PaymentPage { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseClass { get; set; }
        public string ResponseDescription { get; set; }
        public string ResponseClassDescription { get; set; }
        public string TransactionID { get; set; }
        public ComTrustBalance Balance { get; set; }
        public ComTrustAmount Amount { get; set; }
        public ComTrustFees Fees { get; set; }
        public object Payer { get; set; }
        public string UniqueID { get; set; }
    }   

    public class ComTrustTransactionResponseStatus //TransactionResponceStatus
    {
        public string PaymentFrom { get; set; }
        public string Status { get; set; }
        public string Amount { get; set; }
        public string OrderId { get; set; }
        public string TransactionId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public string ErrorCode { get; set; }
        public string TowerURLName { get; set; }
        public string PGType { get; set; }
    }

    public class ComTrustFinalization //Finalization
    {
        public string TransactionID { get; set; }
        public string Customer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class ComTrustFinalizeRequest //FinalizeRequest
    {
        public ComTrustFinalization Finalization { get; set; }
    }

    public class ComTrustPayer //Payer
    {
        public string Information { get; set; }
    }

    public class ComTrustFinalizeTransaction //FinzlizeTransaction
    {
        public string ResponseCode { get; set; }
        public string ResponseClass { get; set; }
        public string ResponseDescription { get; set; }
        public string ResponseClassDescription { get; set; }
        public string Language { get; set; }
        public string ApprovalCode { get; set; }
        public string Account { get; set; }
        public ComTrustBalance Balance { get; set; }
        public string OrderID { get; set; }
        public ComTrustAmount Amount { get; set; }
        public ComTrustFees Fees { get; set; }
        public string CardNumber { get; set; }
        public ComTrustPayer Payer { get; set; }
        public string CardToken { get; set; }
        public string CardBrand { get; set; }
        public string UniqueID { get; set; }
    }

    public class ComTrustFinalizeResponse //FinalizeResponse
    {
        public ComTrustFinalizeTransaction Transaction { get; set; }
    }

    public class ComTrustRequestData
    {

    }
}
