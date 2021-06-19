using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DTO
{
    public class TowerDTO
    {
        public int Id { get; set; }
        public string TowerName { get; set; }
        public int CompanyId { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public int ModifiedBy { get; set; }

        public virtual CompanyDTO Company { get; set; }
        public string ImageURL { get; set; }
        public string NavigationURL { get; set; }
        public int ChargeTypeID { get; set; }
        public string PageFlow { get; set; }
        public string TowerCode { get; set; }
        public int TowerID { get; set; }
    }
}
