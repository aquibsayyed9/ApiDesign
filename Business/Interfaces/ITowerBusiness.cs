using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Business.Interfaces
{
    public interface ITowerBusiness
    {
        List<TowerDTO> GetTowers(int companyID, int chargeTypeId);
        TowerDTO GetTower(int towerId, int chargeTypeId);
    }
}
