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
    public class ParameterDataAccess : IParameterDataAccess
    {
        private IParameterRepository _parameterRepository;
        private IMapper _mapper;

        public ParameterDataAccess(IParameterRepository parameterRepository, IMapper mapper)
        {
            this._parameterRepository = parameterRepository;
            this._mapper = mapper;
        }

        public List<ParameterDTO> GetParameters(int companyID, int chargeTypeID)
        {
            var result = _parameterRepository.FindBy(x => x.CompanyId == companyID && x.ChargeTypeId == 1 && x.Active == true).ToList();
            return _mapper.Map<List<ParameterMaster>, List<ParameterDTO>>(result);
        }
    }
}
