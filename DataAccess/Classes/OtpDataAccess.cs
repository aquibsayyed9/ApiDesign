using AutoMapper;
using EzPay.DataAccess.Interfaces;
using EzPay.DTO;
using EzPay.Entities;
using EzPay.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzPay.DataAccess.Classes
{
    public class OtpDataAccess : IOtpDataAccess
    {
        private IOtpTransactionRepository _otpRepository;
        private IMapper _mapper;
        static Logger logger = LogManager.GetCurrentClassLogger();

        public OtpDataAccess(IOtpTransactionRepository otpRepository, IMapper mapper)
        {
            _otpRepository = otpRepository;
            _mapper = mapper;
        }
        public void InsertOtp(OtpTransactionDto otpTxn)
        {
            try
            {
                var otpData = _mapper.Map<OtpTransactionDto, OtpTransaction>(otpTxn);
                _otpRepository.Add(otpData);
                _otpRepository.Save();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }

        public OtpTransactionDto GetOtpData(OtpTransactionDto otpTxn)
        {
            try
            {
                //OtpTransaction otpData = _mapper.Map<OtpTransactionDto, OtpTransaction>(otpTxn);
                //var result = _otpRepository.GetList(otpData).OrderByDescending(x => x.ModifiedDate).FirstOrDefault();
                var result = _otpRepository.FindBy(x => x.AccountNo == otpTxn.AccountNo).OrderByDescending(x => x.ModifiedDate).FirstOrDefault();
                var otpDto = _mapper.Map<OtpTransaction, OtpTransactionDto>(result);
                return otpDto;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }
    }
}
