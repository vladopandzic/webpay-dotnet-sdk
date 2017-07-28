using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPay.Exceptions
{
    public class IntergrationException : Exception
    {

        public IntergrationException()
        {
        }

        public IntergrationException(String message) : base(message)
        {
        }

        public  IntergrationException(String message, Exception inner) : base(message, inner)
        {
        }
    };
}
