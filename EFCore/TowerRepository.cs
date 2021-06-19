using EzPay.Entities;
using EzPay.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.EFCore
{
    public class TowerRepository : GenericRepository<TowerMaster>, ITowerRepository
    {
        private DbContext _context;
        public TowerRepository(DbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
