using EzPay.Entities;
using EzPay.DTO;
using EzPay.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Repository
{
    public interface ICompanyRepository : IGenericRepository<CompanyMaster>
    {
        //public IEnumerable<CompanyDTO> GetCompanyData();
    }
}
