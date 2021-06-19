using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using EzPay.Business.Interfaces;
using EzPay.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NLog;

namespace EzPayService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RegionController : ControllerBase
    {
        private IRegionBusiness _regionBusiness;
        private readonly AppSettings _appSettings;
        public static Logger _logger = LogManager.GetCurrentClassLogger();
        public RegionController(IOptions<AppSettings> appSettings, IRegionBusiness regionBusiness)
        {
            this._regionBusiness = regionBusiness;
            this._appSettings = appSettings.Value;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetRegions()
        //{
        //    try
        //    {
        //        var regions = await Task.FromResult(_regionBusiness.GetAllRegions());
        //        return Ok(regions);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex);
        //        throw ex;
        //    }
        //}
        [HttpGet]
        public async Task<IActionResult> GetRegions(string latitude = "", string longitude = "")
        {
            try
            {
                var regions = await Task.FromResult(_regionBusiness.GetRegionByCoord(latitude, longitude));
                return Ok(regions);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw ex;
            }
        }
    }
}