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
    public class TowerBusiness : ITowerBusiness
    {
        ITowerDataAccess _towerDataAccess;
        private readonly AppSettings _appSettings;
        public static Logger _logger = LogManager.GetCurrentClassLogger();
        public TowerBusiness(ITowerDataAccess towerDataAccess, IOptions<AppSettings> appSettings)
        {
            this._towerDataAccess = towerDataAccess;
            this._appSettings = appSettings.Value;
        }

        public List<TowerDTO> GetTowers(int companyID, int chargeTypeId)
        {
            var result = _towerDataAccess.GetTowers(companyID, chargeTypeId);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].NavigationURL = $"{_appSettings.appURL}{_appSettings.parameterComponentName}?companyID={result[i].CompanyId}&chargeTypeID={chargeTypeId}";
                result[i].ChargeTypeID = chargeTypeId;
            }
            return result;
        }
        public TowerDTO GetTower(int towerId, int chargeTypeId)
        {
            return _towerDataAccess.GetTower(towerId, chargeTypeId);
        }
    }
}
