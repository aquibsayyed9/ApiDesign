using EzPay.Entities;
using EzPay.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.EFCore
{
    public class PaymentGatewayRepository : GenericRepository<PaymentGatewayMaster>, IPaymentGatewayRepository
    {
        private DbContext _context;
        public PaymentGatewayRepository(DbContext context) : base(context)
        {
            this._context = context;

        }
        public void GetPaymentGatewayDetails(int companyID, int chargeTypeID, int towerID)
        {
            throw new NotImplementedException();
        }
    }
}
