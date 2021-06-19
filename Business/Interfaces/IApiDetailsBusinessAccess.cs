using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Business.Interfaces
{
    public interface IApiDetailsBusinessAccess
    {
        APIDetailsDTO GetApiDetails(int CompanyID, int ChargesTypeID, int TowerID);
    }
}
