using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzPay.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace EzPayService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChargeTypeController : ControllerBase
    {
        private IChargeTypeBusiness _chargeTypeBusiness;
        public static Logger _logger = LogManager.GetCurrentClassLogger();
        public ChargeTypeController(IChargeTypeBusiness chargeTypeBusiness)
        {
            this._chargeTypeBusiness = chargeTypeBusiness;
        }        
        public async Task<IActionResult> GetChargeTypes(int CompanyID, string pageFlow)
        {
            try
            {
                _logger.Info("Inside GetChargeTypes");
                var result = await Task.FromResult(_chargeTypeBusiness.GetCharges(CompanyID, pageFlow));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                throw;
            }
        }
    }
}