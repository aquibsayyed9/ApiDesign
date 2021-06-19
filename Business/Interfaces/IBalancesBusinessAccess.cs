using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EzPay.Business.Interfaces
{
    public interface IBalancesBusinessAccess
    {
        Task<Root> GetCustomerBalance(string accountNo, int companyId, int towerId);
        object PaymentInitiate(PaymentTransactionsDTO paymentDto);
    }
}
