using AutoMapper;
using EzPay.Entities;
using EzPay.DataAccess.Interfaces;
using EzPay.DTO;
using EzPay.Repository;
using EzPay.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzPay.DataAccess.Classes
{
    public class CompanyDataAccess : ICompanyDataAccess
    {
        private ICompanyRepository _companyRepository;
        private IMapper _mapper;

        public CompanyDataAccess(ICompanyRepository companyRepository, IMapper mapper)
        {
            this._companyRepository = companyRepository;
            this._mapper = mapper;
        }
        public List<CompanyDTO> GetCompany()
        {
            var result = _companyRepository.GetAll().ToList();
            return _mapper.Map<List<CompanyMaster>, List<CompanyDTO>>(result);
            //throw new NotImplementedException();
        }

        public List<CompanyDTO> GetCompaniesByRegion(int regionID)
        {
            var result = _companyRepository.FindBy(x => x.RegionID == regionID && x.Active == true).ToList();
            return _mapper.Map<List<CompanyMaster>, List<CompanyDTO>>(result);
        }
        public CompanyDTO GetCompanyById(int companyId)
        {
            var result = _companyRepository.FindBy(x => x.Id == companyId).FirstOrDefault();
            return _mapper.Map<CompanyMaster, CompanyDTO>(result);
        }
    }
}
