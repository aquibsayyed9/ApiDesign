using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DTO
{
    public class AppSettings
    {
        public string appURL { get; set; }
        public string towerComponentName { get; set; }
        public string companyComponentName { get; set; }
        public string chargeTypeComponentName { get; set; }
        public string parameterComponentName { get; set; }
        public string balanceComponentName { get; set; }
        public string getBalanceUrl { get; set; }

        public string ComTrustGateWayUrl { get; set; }
        public string ComTrustReturnPath { get; set; }
        public string LogCT_PG_REQ { get; set; }
        public string CyberSourceGatewayURL { get; set; }
        public string pgReturnPath { get; set; }
        public string successPage { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
        public string FromEmail { get; set; }
        public string SuccessEmailSub { get; set; }
        public string IsEmailSendActive { get; set; }
        public string BccSuccessEmail { get; set; }
        public string otpLength { get; set; }
        public string otpAllowedCharacters { get; set; }
        public string dummyAdminEmail { get; set; }
    }
}
