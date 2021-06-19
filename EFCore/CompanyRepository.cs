using EzPay.Entities;
using EzPay.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace EzPay.EFCore
{
    public class CompanyRepository : GenericRepository<CompanyMaster>, ICompanyRepository
    {
        //private EzPayContext _ezPayContext;
        private DbContext _context;
        public CompanyRepository(DbContext context) : base(context)
        {
            this._context = context;

        }

        //public IEnumerable<CompanyDTO> GetCompanyData()
        //{
        //    var result = _ezPayContext.CompanyMaster.ToList();
        //    return (IEnumerable<CompanyDTO>)result;
        //}
    }
}
