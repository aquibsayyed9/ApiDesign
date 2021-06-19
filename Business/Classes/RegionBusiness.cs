using EzPay.Business.Interfaces;
using EzPay.DataAccess.Interfaces;
using EzPay.DTO;
using Microsoft.Extensions.Options;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzPay.Business.Classes
{
    public class RegionBusiness : IRegionBusiness
    {
        private IRegionDataAccess _regionDataAccess;
        private readonly AppSettings _appSettings;
        public static Logger _logger = LogManager.GetCurrentClassLogger();
        public RegionBusiness(IOptions<AppSettings> appSettings, IRegionDataAccess regionDataAccess)
        {
            this._regionDataAccess = regionDataAccess;
            this._appSettings = appSettings.Value;
        }

        public List<RegionDTO> GetAllRegions()
        {
            var result = _regionDataAccess.GetAllRegions();
            for (int i = 0; i < result.Count; i++)
            {
                result[i].NavigationURL = $"{_appSettings.appURL}{_appSettings.companyComponentName}?regionID={result[i].Id}";
                result[i].IsRegionFound = false;
            }
            return result;
        }

        public List<RegionDTO> GetRegionByCoord(string lat, string lon)
        {
            //var result = _regionDataAccess.GetRegionByCoord(lat, lon);
            var result = _regionDataAccess.GetAllRegions();
            for (int i = 0; i < result.Count; i++)
            {
                result[i].NavigationURL = $"{_appSettings.appURL}{_appSettings.companyComponentName}?regionID={result[i].Id}";
                if (result[i].Latitude == lat && result[i].Longitude == lon)
                    result[i].IsRegionFound = true;
                else
                    result[i].IsRegionFound = false;
            }
            return result;
        }
    }
}
