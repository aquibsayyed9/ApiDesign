using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using EzPay.Business.Interfaces;
using EzPay.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;

namespace EzPayService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PaymentController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        public static Logger _logger = LogManager.GetCurrentClassLogger();
        private IPaymentBusiness _paymentBusiness;
        public PaymentController(IOptions<AppSettings> appSettings, IPaymentBusiness paymentBusiness)
        {
            _appSettings = appSettings.Value;
            _paymentBusiness = paymentBusiness;
        }

        public void RedirectToPaymentGateway(PaymentTransactionsDTO payment)
        {
            _logger.Info("Controller: RedirectToPaymentGateway called, Calling Method: RedirectToPaymentGateway");
        }
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        //public IActionResult TransactionUpdate([FromForm] string TransactionID)
        public IActionResult TransactionUpdate(IFormCollection formdata)
        {
            _logger.Info("Transaction Update Controller Called");
            string txnId = Convert.ToString(formdata["TransactionID"]);
            if (!string.IsNullOrEmpty(txnId))
            //if (!string.IsNullOrEmpty(TransactionID))
            {
                string transactionId = txnId;

                var response = _paymentBusiness.ComTrustFinalize(txnId);
                if (response != null && response.Transaction.ResponseClassDescription == "Success")
                {
                    _paymentBusiness.UpdateGatewayStatus(transactionId, "Success");
                    string redirectUrl = $"{_appSettings.appURL}{_appSettings.successPage}?TransactionID={txnId}&status=success&message=success";

                    StringBuilder emailBody = new StringBuilder(System.IO.File.ReadAllText(@$"{Directory.GetCurrentDirectory()}\Templates\paymentSuccessTemplate.html"));
                    _paymentBusiness.sendSuccessPaymentEmail(transactionId, emailBody.ToString());

                    return RedirectPermanent(redirectUrl);
                }
                else
                {
                    _paymentBusiness.UpdateGatewayStatus(transactionId, "Failure");
                    string redirectUrl = $"{_appSettings.appURL}{_appSettings.successPage}?TransactionID={txnId}&status=failure&message={response.Transaction.ResponseDescription}";
                    return RedirectPermanent(redirectUrl);
                }
                
            }
            else
            {
                _paymentBusiness.UpdateGatewayStatus("", "Failure");
                _logger.Info("TransactionID parameter received as null or empty");
                return NoContent();
            }
            //return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> PayAmount([FromBody] dynamic paymentDto)
        {
            try
            {

                string requestString = Convert.ToString(paymentDto).Replace("{\"paymentDto\":", "");
                requestString = requestString.Remove(requestString.Length - 1, 1);
                var serializedRequest = JsonConvert.DeserializeObject<PaymentTransactionsDTO>(requestString);
                serializedRequest.ReturnPath = _appSettings.pgReturnPath;
                var result = await Task.FromResult(_paymentBusiness.RedirectToPaymentGateway(serializedRequest));
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetDataByTransactionID(string TransactionID)
        {
            try
            {
                PaymentTransactionsDTO transactions = new PaymentTransactionsDTO() { TransactionID = TransactionID };
                var result = await Task.FromResult(_paymentBusiness.GetPaymentTransaction(transactions));
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
