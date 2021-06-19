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
    public class TowerController : ControllerBase
    {        
        private ITowerBusiness _towerBusiness;        
        public static Logger _logger = LogManager.GetCurrentClassLogger();
        public TowerController(ITowerBusiness towerBusiness)
        {
            this._towerBusiness = towerBusiness;
        }
        [HttpGet]
        public async Task<IActionResult> GetTowers(int companyID, int chargeTypeId)
        {
            try
            {
                var towers = await Task.FromResult(_towerBusiness.GetTowers(companyID, chargeTypeId));
                return Ok(towers);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                throw ex;
            }
        }
    }
}