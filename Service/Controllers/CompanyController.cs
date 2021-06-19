using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzPay.Business.Interfaces;
using EzPay.Entities;
using EzPay.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace EzPayService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompanyController : Controller
    {
        //private ICompanyRepository _companyRepository;
        private ICompanyBusiness _companyBusiness;
        //private readonly ILogger<CompanyController> _logger;
        public static Logger _logger = LogManager.GetCurrentClassLogger();
        public CompanyController(ICompanyBusiness companyBusiness)
        {
            this._companyBusiness = companyBusiness;
            //this._logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCompany()
        {
            try
            {
                _logger.Info("Inside GetCompany()");
                
                var company = await Task.FromResult(_companyBusiness.GetAllCompany());
                return Ok(company);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies(int regionID)
        {
            try
            {              
                var company = await Task.FromResult(_companyBusiness.GetCompaniesByRegion(regionID));
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