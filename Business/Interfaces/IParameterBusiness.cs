using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Business.Interfaces
{
    public interface IParameterBusiness
    {
        public List<ParameterDTO> GetParameters(int companyID, int chargeTypeID);
    }
}
