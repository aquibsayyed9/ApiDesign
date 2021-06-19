using EzPay.Entities;
using EzPay.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.EFCore
{
    public class RegionRepository : GenericRepository<RegionMaster>, IRegionRepository
    {
        private DbContext _context;
        public RegionRepository(DbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }
    }
}
