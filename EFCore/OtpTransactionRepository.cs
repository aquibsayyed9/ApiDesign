using EzPay.Entities;
using EzPay.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.EFCore
{
    public class OtpTransactionRepository : GenericRepository<OtpTransaction>, IOtpTransactionRepository
    {
        private DbContext _context;
        public OtpTransactionRepository(DbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
