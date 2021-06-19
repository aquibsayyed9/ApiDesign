﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DTO
{
    public class ChargeTypeDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string ChargeType { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public int ModifiedBy { get; set; }
        public string Image { get; set; }
        public string NavigationURL { get; set; }
        public string PageFLow { get; set; }
        public string ChargeTypeCode { get; set; }
    }
}
