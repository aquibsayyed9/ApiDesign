using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DataAccess.Interfaces
{
    public interface IOtpDataAccess
    {
        void InsertOtp(OtpTransactionDto otpTxn);
        OtpTransactionDto GetOtpData(OtpTransactionDto otpTxn);
    }
}
