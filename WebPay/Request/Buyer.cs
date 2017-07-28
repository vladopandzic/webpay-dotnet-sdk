using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPay.Request
{
    public class Buyer
    {
        public string FullName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string ZIP { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

    }
}
