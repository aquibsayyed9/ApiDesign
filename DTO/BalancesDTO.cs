using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DTO
{
    public class BalancesDTO
    {
        public string ProfitCenter { get; set; }
        public string CompanyCode { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string TowerType { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class CustomerDetails
    {
        public int CompanyCode { get; set; }
        public object ContractNumber { get; set; }
        public string CustomerNumber { get; set; }
        public string ProfitCenter { get; set; }
        public string CustomerName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public double TotalAmount { get; set; }
        public object GLAccount { get; set; }
        public object Description { get; set; }
        public object PaymentType { get; set; }
        public object Tower { get; set; }
        public object PaymentFrom { get; set; }
        public object IPAddress { get; set; }
        public int ChargeTypeId { get; set; }
        public int TowerId { get; set; }
        public int CompanyId { get; set; }
    }

    public class FlowType
    {
        public string flowType { get; set; }
        public double Amount { get; set; }
        public int PriorityId { get; set; }
        public string DebitCredit { get; set; }
        public string DocDate { get; set; }
        public string ItemText { get; set; }
    }

    public class Unit
    {
        public string UnitNumber { get; set; }
        public double DueAmount { get; set; }
        public double Amount { get; set; }
        public string ContractNumber { get; set; }
        public List<FlowType> FlowTypes { get; set; }
        public string ItemText { get; set; }
    }

    public class Result
    {
        public CustomerDetails CustomerDetails { get; set; }
        public List<Unit> Units { get; set; }
    }

    public class Root
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public Result Result { get; set; }
        public object Param { get; set; }
    }


}
