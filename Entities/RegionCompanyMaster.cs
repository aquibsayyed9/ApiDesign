using System;
using System.Collections.Generic;

namespace EzPay.Entities
{
    public partial class RegionCompanyMaster
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public int CompanyId { get; set; }
    }
}
