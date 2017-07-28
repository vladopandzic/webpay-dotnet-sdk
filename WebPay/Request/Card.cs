using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPay.Request
{
    public class Card
    {
        public string Pan { get; set; }

        public int CVV { get; set; }

        public string ExpirationDate { get; set; }
    }
}
