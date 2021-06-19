using EzPay.Entities;
using EzPay.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.EFCore
{
    public class ChargeTypeRepository : GenericRepository<ChargeTypeMaster>, IChargeTypeRepository
    {
        private DbContext _context;
        public ChargeTypeRepository(DbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
