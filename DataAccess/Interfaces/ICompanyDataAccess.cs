using EzPay.Entities;
using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DataAccess.Interfaces
{
    public interface ICompanyDataAccess
    {
        List<CompanyDTO> GetCompany();
        List<CompanyDTO> GetCompaniesByRegion(int regionID);
        CompanyDTO GetCompanyById(int companyId);
    }
}
