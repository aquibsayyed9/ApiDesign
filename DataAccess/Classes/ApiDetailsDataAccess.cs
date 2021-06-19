using AutoMapper;
using EzPay.DataAccess.Interfaces;
using EzPay.DTO;
using EzPay.Entities;
using EzPay.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzPay.DataAccess.Classes
{
    public class ApiDetailsDataAccess : IApiDetailsDataAccess
    {
        private IApiDetailsRepository _apiDetailsRepository;
        private IMapper _mapper;

        public ApiDetailsDataAccess(IApiDetailsRepository apiDetailsRepository, IMapper mapper)
        {
            _apiDetailsRepository = apiDetailsRepository;
            _mapper = mapper;
        }

        public APIDetailsDTO GetApiDetails(int CompanyID, int ChargeTypeID, int TowerID)
        {
            var result = _apiDetailsRepository.FindBy(x => x.CompanyId == CompanyID && x.ChargeTypeId == ChargeTypeID && x.TowerId == TowerID && x.Active == true).FirstOrDefault();
            return _mapper.Map<Apidetails, APIDetailsDTO>(result);
        }
    }
}
