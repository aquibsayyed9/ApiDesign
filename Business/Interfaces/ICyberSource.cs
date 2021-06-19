using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Business.Interfaces
{
    public interface ICyberSource
    {
        CyberSourceDTO GenerateCSModel(PaymentTransactionsDTO paymentDTO, string amount);

        CyberSourceDTO GetSignedData(CyberSourceDTO model, out bool status);
    }
}
