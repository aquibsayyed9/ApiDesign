using System;
using System.Collections.Generic;

namespace EzPay.DTO
{
    public class CompanyDTO
    {
        public CompanyDTO()
        {
            towerDTO = new HashSet<TowerDTO>();
        }
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public int RegionID { get; set; }
        public string CompanyName { get; set; }
        public int LogoPosition { get; set; }
        public string PageFlow { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public int ModifiedBy { get; set; }
        public virtual ICollection<TowerDTO> towerDTO { get; set; }
        public string Image { get; set; }
        public string NavigationURL { get; set; }
        public bool IsOtpEnabled { get; set; }
    }
}
