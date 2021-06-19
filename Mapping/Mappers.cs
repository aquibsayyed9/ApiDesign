using AutoMapper;
using EzPay.Entities;
using EzPay.DTO;
using System;

namespace EzPay.Mapping
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<CompanyMaster, CompanyDTO>();
            CreateMap<RegionMaster, RegionDTO>();
            CreateMap<TowerMaster, TowerDTO>();
            CreateMap<ChargeTypeMaster, ChargeTypeDTO>();
            CreateMap<ParameterMaster, ParameterDTO>();
            CreateMap<Apidetails, APIDetailsDTO>();

            CreateMap<PaymentGatewayMaster, PaymentGatewayDTO>();
            CreateMap<PaymentGatewayDTO, PaymentGatewayMaster>();

            CreateMap<PaymentTransactions, PaymentTransactionsDTO>();
            CreateMap<PaymentTransactionsDTO, PaymentTransactions>();

            CreateMap<OtpTransactionDto, OtpTransaction>();
            CreateMap<OtpTransaction, OtpTransactionDto>();
        }
    }
}
