using EzPay.Business.Interfaces;
using EzPay.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzPayService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private IOtpTransactionBusiness _otpBusiness;
        private readonly AppSettings _appSettings;

        public static Logger _logger = LogManager.GetCurrentClassLogger();

        public OtpController(IOtpTransactionBusiness otpBusiness, IOptions<AppSettings> appSettings)
        {
            _otpBusiness = otpBusiness;
            _appSettings = appSettings.Value;
        }
        public async Task<IActionResult> GenerateOtp(string accountNo, int chargeTypeID, int towerID)
        {
            try
            {
                await Task.Run(() => _otpBusiness.InsertOtp(accountNo, chargeTypeID, towerID));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult VerifyOtp(dynamic otpTransaction)
        {
            try
            {
                string requestString = Convert.ToString(otpTransaction).Replace("{\"otpTransaction\":", "");
                requestString = requestString.Remove(requestString.Length - 1, 1);
                var serializedRequest = JsonConvert.DeserializeObject<OtpTransactionDto>(requestString);

                var result =_otpBusiness.VerifyOtp(serializedRequest);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }
    }
}
