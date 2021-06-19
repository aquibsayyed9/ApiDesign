using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DataAccess.Interfaces
{
    public interface IRegionDataAccess
    {
        List<RegionDTO> GetAllRegions();
        List<RegionDTO> GetRegionByCoord(string lat, string lon);
    }
}
