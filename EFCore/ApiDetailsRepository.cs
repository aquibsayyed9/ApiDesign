using EzPay.Entities;
using EzPay.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.EFCore
{
    public class ApiDetailsRepository : GenericRepository<Apidetails>, IApiDetailsRepository
    {
        private DbContext _context;
        public ApiDetailsRepository(DbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
