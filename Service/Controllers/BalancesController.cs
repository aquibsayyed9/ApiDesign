using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
//using System.Web.Http;
using System.Web.Http.Cors;
using EzPay.Business.Interfaces;
using EzPay.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

namespace EzPayService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BalancesController : ControllerBase
    {
        private IBalancesBusinessAccess _balancesBusinessAccess;
        private readonly AppSettings _appSettings;
        public static Logger _logger = LogManager.GetCurrentClassLogger();

        public BalancesController(IBalancesBusinessAccess balancesBusinessAccess, IOptions<AppSettings> appSettings)
        {
            _balancesBusinessAccess = balancesBusinessAccess;
            _appSettings = appSettings.Value;
        }
        public async Task<IActionResult> GetBalances(string accountNo, int chargeTypeID, int towerID)
        {
            try
            {
                var customerBalances = await Task.FromResult(_balancesBusinessAccess.GetCustomerBalance(accountNo, chargeTypeID, towerID));
                return Ok(customerBalances);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }                
    }
}
