using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPay.Exceptions
{
   
    public class TransactionTypeMismatchException : Exception
    {

        public TransactionTypeMismatchException()
        {
        }

        public TransactionTypeMismatchException(String message) : base(message)
        {
        }

        public TransactionTypeMismatchException(String message, Exception inner) : base(message, inner)
        {
        }
    };
}
