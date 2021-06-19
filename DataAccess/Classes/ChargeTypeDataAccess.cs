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
    public class ChargeTypeDataAccess : IChargeTypeDataAccess
    {
        private IChargeTypeRepository _chargeTypeRepository;
        private IMapper _mapper;

        public ChargeTypeDataAccess(IChargeTypeRepository chargeTypeRepository, IMapper mapper)
        {
            this._chargeTypeRepository = chargeTypeRepository;
            this._mapper = mapper;
        }

        public List<ChargeTypeDTO> GetChargeTypeByCompany(int regionID)
        {
            var result = _chargeTypeRepository.FindBy(x => x.CompanyId == regionID && x.Active == true).ToList();
            return _mapper.Map<List<ChargeTypeMaster>, List<ChargeTypeDTO>>(result);
        }

        public ChargeTypeDTO GetChargeById(int chargeTypeId)
        {
            var result = _chargeTypeRepository.FindBy(x => x.Id == chargeTypeId).FirstOrDefault();
            return _mapper.Map<ChargeTypeMaster, ChargeTypeDTO>(result);
        }
    }
}
