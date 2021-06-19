using AutoMapper;
using EzPay.DataAccess.Interfaces;
using EzPay.DTO;
using EzPay.Entities;
using EzPay.Repository;
using EzPay.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzPay.DataAccess.Classes
{
    public class TowerDataAccess : ITowerDataAccess
    {
        private ITowerRepository _towerRepository;
        private IMapper _mapper;

        public TowerDataAccess(ITowerRepository companyRepository, IMapper mapper)
        {
            this._towerRepository = companyRepository;
            this._mapper = mapper;
        }

        public List<TowerDTO> GetTowers(int companyID, int chargeTypeId)
        {
            //var result = _towerRepository.FindBy(x => x.CompanyId == companyID && x.Active == true).GroupBy(tower => tower.TowerID).Select(grp => grp.First()).ToList();
            var result = _towerRepository.FindBy(x => x.CompanyId == companyID && x.Active == true && x.ChargeTypeID == chargeTypeId).ToList();
            return _mapper.Map<List<TowerMaster>, List<TowerDTO>>(result);
        }
        public TowerDTO GetTower(int towerId, int chargeTypeId)
        {
            var result = _towerRepository.FindBy(x => x.TowerID == towerId && x.ChargeTypeID == chargeTypeId).FirstOrDefault();
            return _mapper.Map<TowerMaster, TowerDTO>(result);
        }
    }
}
