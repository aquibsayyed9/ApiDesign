using EzPay.Business.Interfaces;
using EzPay.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EzPay.Business.Classes
{
    public class BalancesBusinessAccess : IBalancesBusinessAccess
    {
        private IApiDetailsBusinessAccess _apiDetailsBusinessAccess;
        private ICompanyBusiness _companyBusiness;
        private ITowerBusiness _towerBusiness;
        private IChargeTypeBusiness _chargeTypeBusiness;
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        public IPaymentBusiness _paymentBusiness;
        public static Logger _logger = LogManager.GetCurrentClassLogger();
        public BalancesBusinessAccess(IApiDetailsBusinessAccess apiDetailsBusinessAccess, HttpClient httpClient, IOptions<AppSettings> appSettings, IPaymentBusiness paymentBusiness
            , ICompanyBusiness companyBusiness, ITowerBusiness towerBusiness, IChargeTypeBusiness chargeTypeBusiness)
        {
            this._apiDetailsBusinessAccess = apiDetailsBusinessAccess;
            this._httpClient = httpClient;
            this._appSettings = appSettings.Value;
            this._paymentBusiness = paymentBusiness;
            this._companyBusiness = companyBusiness;
            this._towerBusiness = towerBusiness;
            this._chargeTypeBusiness = chargeTypeBusiness;
        }

        public APIDetailsDTO GetApiDetails(int companyId, int chargeTypeId, int towerId)
        {
            try
            {
                return _apiDetailsBusinessAccess.GetApiDetails(companyId, chargeTypeId, towerId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }
        public async Task<Root> GetCustomerBalance(string accountNo, int chargeTypeId, int towerId)
        {
            try
            {
                string profitCenter = _towerBusiness.GetTower(towerId, chargeTypeId).TowerCode;
                string companyCode = _chargeTypeBusiness.GetChargeById(chargeTypeId).ChargeTypeCode;
                //var data = new StringContent("{\"ProfitCenter\":\"1104200001\",\"CompanyCode\":\"1104\",\"CustomerAccountNumber\":\"" + accountNo + "\",\"TowerType\":\"Standard\"}", Encoding.UTF8, "application/json");
                var data = new StringContent("{\"ProfitCenter\":\"" + profitCenter + "\",\"CompanyCode\":\"" + companyCode + "\",\"CustomerAccountNumber\":\"" + accountNo + "\",\"TowerType\":\"Standard\"}", Encoding.UTF8, "application/json");
                var responseString = await _httpClient.PostAsync(_appSettings.getBalanceUrl, data).Result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<Root>(responseString);
                //response.Result.CustomerDetails.ChargeTypeId = 
                return response;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }
        public object PaymentInitiate(PaymentTransactionsDTO paymentDto)
        {
            try
            {
                return _paymentBusiness.RedirectToPaymentGateway(paymentDto);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }
    }
}
