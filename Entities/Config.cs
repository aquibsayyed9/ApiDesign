using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Entities
{
    public class Config
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool IsEncrypt { get; set; }
    }
}
