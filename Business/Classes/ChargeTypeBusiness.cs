using EzPay.Business.Interfaces;
using EzPay.DataAccess.Interfaces;
using EzPay.DTO;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Business.Classes
{
    public class ChargeTypeBusiness : IChargeTypeBusiness
    {
        IChargeTypeDataAccess _chargeTypeDataAccess;
        private readonly AppSettings _appSettings;
        public ChargeTypeBusiness(IChargeTypeDataAccess chargeDataAccess, IOptions<AppSettings> appSettings)
        {
            this._chargeTypeDataAccess = chargeDataAccess;
            this._appSettings = appSettings.Value;
        }

        public List<ChargeTypeDTO> GetCharges(int CompanyID, string pageFlow)
        {
            var result = _chargeTypeDataAccess.GetChargeTypeByCompany(CompanyID);
            for (int i = 0; i < result.Count; i++)
            {
                if(pageFlow.Contains('4'))
                    result[i].NavigationURL = $"{_appSettings.appURL}{_appSettings.towerComponentName}?companyID={result[i].CompanyId}&ChargeTypeId={result[i].Id}&PageFlow={pageFlow}";
                else
                    result[i].NavigationURL = $"{_appSettings.appURL}{_appSettings.parameterComponentName}?companyID={result[i].CompanyId}&ChargeTypeId={result[i].Id}&PageFlow={pageFlow}";
            }
            return result;
        }
        public ChargeTypeDTO GetChargeById(int chargeTypeId)
        {
            return _chargeTypeDataAccess.GetChargeById(chargeTypeId);
        }
    }
}
