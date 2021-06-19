using EzPay.Business.Interfaces;
using EzPay.DataAccess.Interfaces;
using EzPay.DTO;
using Microsoft.Extensions.Options;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Business.Classes
{
    public class ParameterBusiness : IParameterBusiness
    {
        IParameterDataAccess _parameterDataAccess;
        ICompanyBusiness _companyBusiness;
        private readonly AppSettings _appSettings;
        public static Logger _logger = LogManager.GetCurrentClassLogger();
        public ParameterBusiness(IParameterDataAccess parameterDataAccess, ICompanyBusiness companyBusiness, IOptions<AppSettings> appSettings)
        {
            this._parameterDataAccess = parameterDataAccess;
            this._appSettings = appSettings.Value;
            this._companyBusiness = companyBusiness;
        }
        public List<ParameterDTO> GetParameters(int companyID, int chargeTypeID)
        {
            var result = _parameterDataAccess.GetParameters(companyID, chargeTypeID);
            var isOtpEnabled = _companyBusiness.GetCompanyById(companyID).IsOtpEnabled;
            for (int i = 0; i < result.Count; i++)
            {
                result[i].NavigationURL = $"{_appSettings.appURL}{_appSettings.balanceComponentName}?parameterID={result[i].Id}";
                result[i].IsOtpEnabled = isOtpEnabled;
            }
            return result;
        }
    }
}
