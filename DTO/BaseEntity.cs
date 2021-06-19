using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EzPay.DTO
{
    public abstract class BaseEntity
    {
       
        public long Id { get; set; }
    }
    public abstract class SQLEntity : BaseEntity
    {
       
        public long Id { get; set; }
    } 
}
