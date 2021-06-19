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
    public class ParametersController : ControllerBase
    {
        private IParameterBusiness _parameterBusiness;
        public static Logger _logger = LogManager.GetCurrentClassLogger();

        public ParametersController(IParameterBusiness parameterBusiness)
        {
            _parameterBusiness = parameterBusiness;
        }
        public async Task<IActionResult> GetParameters(int companyID, int chargeTypeID)
        {
            try
            {
                var company = await Task.FromResult(_parameterBusiness.GetParameters(companyID, chargeTypeID));
                return Ok(company);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                throw ex;
            }
        }
    }
}
