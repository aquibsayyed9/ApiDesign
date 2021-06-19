using EzPay.Entities;
using EzPay.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.EFCore
{
    public class PaymentTransactionsRepository : GenericRepository<PaymentTransactions>, IPaymentTransactionsRepository
    {
        private DbContext _context;
        public PaymentTransactionsRepository(DbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
