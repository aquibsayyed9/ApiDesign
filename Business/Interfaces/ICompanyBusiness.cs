using EzPay.DTO;
using System.Collections.Generic;

namespace EzPay.Business.Interfaces
{
    public interface ICompanyBusiness
    {
        List<CompanyDTO> GetAllCompany();
        List<CompanyDTO> GetCompaniesByRegion(int regionID);
        CompanyDTO GetCompanyById(int companyId);
    }
}
