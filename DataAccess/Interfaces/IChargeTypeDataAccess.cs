using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DataAccess.Interfaces
{
    public interface IChargeTypeDataAccess
    {
        public List<ChargeTypeDTO> GetChargeTypeByCompany(int regionID);
        ChargeTypeDTO GetChargeById(int chargeTypeId);
    }
}
