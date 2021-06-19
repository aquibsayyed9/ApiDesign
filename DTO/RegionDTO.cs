using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DTO
{
    public class RegionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public int ModifiedBy { get; set; }
        public string Image { get; set; }
        public string NavigationURL { get; set; }
        public bool IsRegionFound { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
