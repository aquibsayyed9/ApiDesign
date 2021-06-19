using System;
using System.Collections.Generic;

namespace EzPay.Entities
{
    public partial class TowerMaster
    {
        public int Id { get; set; }
        public string TowerName { get; set; }
        public int CompanyId { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public int ModifiedBy { get; set; }
        public string TowerCode { get; set; }
        public virtual CompanyMaster Company { get; set; }
        public int ChargeTypeID { get; set; }
        public int TowerID { get; set; }
    }
}
