using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebPay.Request
{
    [XmlRoot("transaction")]
    public class TransactionSimple
    {
        [XmlElement("order_number")]
        public string OrderNumber { get; set; }

        [XmlElement("amount")]
        public double Amount { get; set; }

        [XmlElement("currency")]
        public Currency Currency { get; set; }

        [XmlElement("authenticity_token")]
        public string AuthenticityToken { get; set; }

        [XmlElement("digest")]
        public string Digest { get; set; }
    }
}
