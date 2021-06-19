using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DTO
{
    public class PaymentTransactionsDTO
    {
        public long ID { get; set; }
        public int CompanyID { get; set; }
        public int ChargeTypeID { get; set; }
        public long TowerID { get; set; }
        public string Parameter { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string PhoneNumber { get; set; }
        public double PaidAmount { get; set; }
        public string PaymentFrom { get; set; }
        public string OrderID { get; set; }
        public string TransactionID { get; set; }
        public string GatewayStatus { get; set; }
        public string PGType { get; set; }
        public string PGDetails { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string ApprovalCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDt { get; set; }
        public string IPAddress { get; set; }
        public string ReturnPath { get; set; }
        public List<Unit> unitData { get; set; }
        public string data { get; set; }
    }    
}
