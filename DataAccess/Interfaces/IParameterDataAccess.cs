using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DataAccess.Interfaces
{
    public interface IParameterDataAccess
    {
        public List<ParameterDTO> GetParameters(int companyID, int chargeTypeID);
    }
}
