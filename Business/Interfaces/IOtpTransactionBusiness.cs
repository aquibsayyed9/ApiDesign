using EzPay.DataAccess.Interfaces;
using EzPay.DTO;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Business.Interfaces
{
    public interface IOtpTransactionBusiness
    {
        void InsertOtp(string accountNo, int chargeTypeID, int towerID);
        bool VerifyOtp(OtpTransactionDto otpTxn);
    }
}
