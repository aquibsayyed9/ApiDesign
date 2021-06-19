using EzPay.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Repository.Interfaces
{
    public interface IPaymentGatewayRepository : IGenericRepository<PaymentGatewayMaster>
    {
        void GetPaymentGatewayDetails(int companyID, int chargeTypeID, int towerID);
    }
}
