using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Business.Interfaces
{
    public interface IRegionBusiness
    {
        List<RegionDTO> GetAllRegions();
        List<RegionDTO> GetRegionByCoord(string lat, string lon);
    }
}
