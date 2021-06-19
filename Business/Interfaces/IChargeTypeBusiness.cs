using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Business.Interfaces
{
    public interface IChargeTypeBusiness
    {
        public List<ChargeTypeDTO> GetCharges(int CompanyID, string pageFlow);
        ChargeTypeDTO GetChargeById(int chargeTypeId);
    }
}
