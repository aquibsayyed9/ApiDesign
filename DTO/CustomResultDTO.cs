using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.DTO
{
    public class ObjectParamsDTO
    {
        public string Value { get; set; }
    }
    public class CustomResultDTO<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public ObjectParamsDTO Param { get; set; }
    }
}
