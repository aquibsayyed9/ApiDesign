using EzPay.Business.Interfaces;
using EzPay.DataAccess.Interfaces;
using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Business.Classes
{
    public class ApiDetailsBusinessAccess : IApiDetailsBusinessAccess
    {
        private IApiDetailsDataAccess _apiDetailsDataAccess;

        public ApiDetailsBusinessAccess(IApiDetailsDataAccess apiDetailsDataAccess)
        {
            _apiDetailsDataAccess = apiDetailsDataAccess;
        }

        public APIDetailsDTO GetApiDetails(int CompanyID, int ChargesTypeID, int TowerID)
        {
            return _apiDetailsDataAccess.GetApiDetails(CompanyID, ChargesTypeID, TowerID);
        }
    }
}
