    using EzPay.Business.Interfaces;
using EzPay.DataAccess.Interfaces;
using EzPay.DTO;
using Microsoft.Extensions.Options;
using NLog;
using System.Collections.Generic;

namespace EzPay.Business.Classes
{
    public class CompanyBusiness : ICompanyBusiness
    {
        ICompanyDataAccess _companyDataAccess;
        private readonly AppSettings _appSettings;
        public static Logger _logger = LogManager.GetCurrentClassLogger();
        public CompanyBusiness(ICompanyDataAccess companyDataAccess, IOptions<AppSettings> appSettings)
        {
            this._companyDataAccess = companyDataAccess;
            this._appSettings = appSettings.Value;
        }
        public List<CompanyDTO> GetAllCompany()
        {
            return _companyDataAccess.GetCompany();
            //throw new NotImplementedException();
        }

        public List<CompanyDTO> GetCompaniesByRegion(int regionID)
        {
            var result = _companyDataAccess.GetCompaniesByRegion(regionID);
            for (int i = 0; i < result.Count; i++)
            {
                if(result[i].PageFlow.Contains('3'))
                    result[i].NavigationURL = $"{_appSettings.appURL}{_appSettings.chargeTypeComponentName}?companyID={result[i].Id}&chargeTypeId={result[i].Id}&PageFlow={result[i].PageFlow}";
                else if(result[i].PageFlow.Contains('4'))
                    result[i].NavigationURL = $"{_appSettings.appURL}{_appSettings.towerComponentName}?companyID={result[i].Id}&PageFlow={result[i].PageFlow}";
                else
                    result[i].NavigationURL = $"{_appSettings.appURL}{_appSettings.parameterComponentName}?companyID={result[i].Id}&PageFlow={result[i].PageFlow}";
            }
            return result;
        }

        public CompanyDTO GetCompanyById(int companyId)
        {
            return _companyDataAccess.GetCompanyById(companyId);
        }
    }
}
