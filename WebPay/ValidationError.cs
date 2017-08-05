using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPay
{
    public class ValidationError
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public object CustomState { get; set; }

        public string PropertyName { get; set; }

        public object AttemptedValue { get; set; }
    }
}
