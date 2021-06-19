using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DTO
{
    public class PaymentGatewayDTO
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public int? ChargeTypeID { get; set; }
        public long? TowerID { get; set; }
        public string PGType { get; set; }
        public string MerchantID { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string ProfileID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CustomerName { get; set; }
        public string Store { get; set; }
        public string Terminal { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public int ModifiedBy { get; set; }
    }
}
