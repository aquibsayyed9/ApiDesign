using EzPay.Business.Interfaces;
using EzPay.DataAccess.Interfaces;
using EzPay.DTO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EzPay.Business.Classes
{
    public class OtpTransactionBusiness : IOtpTransactionBusiness
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        IOtpDataAccess _otpDataAccess;
        IBalancesBusinessAccess _balanceBusiness;
        IEmailBusines _emailService;
        private readonly AppSettings _appSettings;

        public OtpTransactionBusiness(IOtpDataAccess otpDataAccess, IOptions<AppSettings> appSettings, IBalancesBusinessAccess balanceBusiness, IEmailBusines emailBusiness)
        {
            _otpDataAccess = otpDataAccess;
            _appSettings = appSettings.Value;
            _balanceBusiness = balanceBusiness;
            _emailService = emailBusiness;
        }

        public void InsertOtp(string accountNo, int chargeTypeID, int towerID)
        {
            try
            {
                var task = Task.Run(() => _balanceBusiness.GetCustomerBalance(accountNo, chargeTypeID, towerID));
                task.Wait();
                var result = task.Result.Result.CustomerDetails;

                OtpTransactionDto data = new OtpTransactionDto() {
                    Email = result.EmailId,
                    MobileNumber = result.MobileNumber,
                    RawData = JsonConvert.SerializeObject(task.Result),
                    Otp = GenerateRandomOTP(),
                    AccountNo = result.CustomerNumber,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };
                
                _otpDataAccess.InsertOtp(data);
                sendOtpEmail(data.Otp, result.EmailId, result.CustomerName);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }
        public bool VerifyOtp(OtpTransactionDto otpTxn)
        {
            try
            {
                var result = _otpDataAccess.GetOtpData(otpTxn);
                if (result.Otp == otpTxn.Otp)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }

        private string GenerateRandomOTP()
        {
            string[] saAllowedCharacters = _appSettings.otpAllowedCharacters.Split(',');
            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();

            for (int i = 0; i < Convert.ToInt32(_appSettings.otpLength); i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;
            }
            return sOTP;
        }
        private void sendOtpEmail(string otp, string email, string customerName)
        {
            try
            {
                logger.Info($"Sending otp email to user: {email}");
                StringBuilder emailBody = new StringBuilder(System.IO.File.ReadAllText(@$"{Directory.GetCurrentDirectory()}\Templates\otpTemplate.html"));
                emailBody = emailBody.Replace("%customerName%", customerName);
                emailBody = emailBody.Replace("%OtpString%", otp);

                _emailService.Send(_appSettings.FromEmail, email, "Otp Verification", emailBody.ToString());
                logger.Info("Otp Email sent successfully");
            }
            catch (Exception ex)
            {
                logger.Error(ex);                
            }
        }
    }
}
