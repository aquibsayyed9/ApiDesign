using EzPay.DataAccess.Classes;
using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DataAccess.Interfaces
{
    public interface IApiDetailsDataAccess
    {
        APIDetailsDTO GetApiDetails(int CompanyID, int ChargeTypeID, int TowerID);
    }
}
