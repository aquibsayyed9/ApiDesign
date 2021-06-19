using System;
using System.Collections.Generic;

namespace EzPay.Entities
{
    public partial class Apidetails
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int? ChargeTypeId { get; set; }
        public int? TowerId { get; set; }
        public string Apiurl { get; set; }
        public string Filters { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public int ModifiedBy { get; set; }
    }
}
