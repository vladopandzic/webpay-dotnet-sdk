using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPay.Request
{
    public enum Currency
    {
        EUR,
        USD,
        BAM,
        HRK

    }

    public class Order
    {
        public string OrderInfo { get; set; }

        public string OrderNumber { get; set; }

        public int Amount { get; set; }

        public Currency Currency { get; set; }
    }
}
