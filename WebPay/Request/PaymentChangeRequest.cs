using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebPay.Request
{
    public class PaymentChangeRequest
    {
        [XmlElement("transaction")]
        public TransactionSimple Transaction { get; set; }
    }
}
