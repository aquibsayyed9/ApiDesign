using EzPay.Entities;
using EzPay.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.EFCore
{
    public class ParameterRepository : GenericRepository<ParameterMaster>, IParameterRepository
    {
        private DbContext _context;
        public ParameterRepository(DbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
