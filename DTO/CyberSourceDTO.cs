using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DTO
{
    public class CyberSourceDTO
    {
        public string access_key { get; set; }
        public string profile_id { get; set; }
        public string transaction_uuid { get; set; }
        public string signed_field_names { get; set; }
        public string unsigned_field_names { get; set; }
        public string signed_date_time { get; set; }
        public string locale { get; set; }
        public string transaction_type { get; set; }
        public string reference_number { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string submit { get; set; }
        public string payment_method { get; set; }
        public string bill_to_forename { get; set; }
        public string bill_to_surname { get; set; }
        public string bill_to_email { get; set; }
        public string bill_to_phone { get; set; }
        public string bill_to_address_line1 { get; set; }
        public string bill_to_address_city { get; set; }
        public string bill_to_address_state { get; set; }
        public string bill_to_address_country { get; set; }
        public string bill_to_address_postal_code { get; set; }
        public string PGType { get; set; }
        public string RequestParams { get; set; }
        public string PGUrl { get; set; }
        public string customer_ip_address { get; set; }
        public int CompanyCode { get; set; }
        public string ProfitCenter { get; set; }
        public string SecretKey { get; set; }
    }

    public class CyberSourceReturnDTO
    {
        public string RequestParams { get; set; }
        public string PGUrl { get; set; }
        public string PGType { get; set; }
    }
}
