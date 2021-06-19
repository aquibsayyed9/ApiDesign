using System;
using System.Collections.Generic;

namespace EzPay.Entities
{
    public partial class ParameterMaster
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int ChargeTypeId { get; set; }
        public string Parameter { get; set; }
        public string DisplayName { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public int ModifiedBy { get; set; }
        public string IconName { get; set; }
    }
}
