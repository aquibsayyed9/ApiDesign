using AutoMapper;
using EzPay.Entities;
using EzPay.DataAccess.Interfaces;
using EzPay.DTO;
using EzPay.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzPay.DataAccess.Classes
{
    public class RegionDataAccess : IRegionDataAccess
    {
        private IRegionRepository _regionRepository;
        private IMapper _mapper;
        public RegionDataAccess(IRegionRepository regionRepository, IMapper mapper)
        {
            this._regionRepository = regionRepository;
            this._mapper = mapper;
        }

        public List<RegionDTO> GetAllRegions()
        {
            var result = _regionRepository.FindBy(x => x.Active == true).ToList();
            return _mapper.Map<List<RegionMaster>, List<RegionDTO>>(result);
        }
        public List<RegionDTO> GetRegionByCoord(string lat, string lon)
        {
            var result = _regionRepository.FindBy(x => x.Active == true && x.Latitude == lat && x.Longitude == lon).ToList();
            return _mapper.Map<List<RegionMaster>, List<RegionDTO>>(result);
        }
    }
}
