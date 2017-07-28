using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebPay.Request
{
    public class PaymentRequest
    {
        [XmlElement("transaction")]
        public Transaction Transaction { get; set; }
    }
}
