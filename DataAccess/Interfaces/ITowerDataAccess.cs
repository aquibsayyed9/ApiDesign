using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DataAccess.Interfaces
{
    public interface ITowerDataAccess
    {
        List<TowerDTO> GetTowers(int companyID, int chargeTypeId);
        TowerDTO GetTower(int towerId, int chargeTypeId);
    }
}
