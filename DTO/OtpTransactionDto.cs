using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DTO
{
    public class OtpTransactionDto
    {
        public int Id { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string RawData { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy
        {
            get; set;
        }
        public string Otp { get; set; }
        public string AccountNo { get; set; }
    }
}
