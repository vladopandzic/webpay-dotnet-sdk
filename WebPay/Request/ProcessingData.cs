using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebPay.Request
{
    public enum Language
    {
        [XmlEnum(Name = "en")]
        EN,
        [XmlEnum(Name = "es")]
        ES,
        [XmlEnum(Name = "ba")]
        BA,
        [XmlEnum(Name = "hr")]
        HR
    }
    public enum TransactionType
    {

        [XmlEnum(Name = "authorize")]
        Authorize,
        [XmlEnum(Name = "purchase")]
        Purchase,
        [XmlEnum(Name = "capture")]
        Capture,
        [XmlEnum(Name = "refund")]
        Refund,
        [XmlEnum(Name = "void")]
        Void
    }

    public class ProcessingData
    {
        public string Ip { get; set; }

        public Language Language { get; set; }

        public TransactionType TransactionType { get; set; }

        public string AuthenticityToken { get; set; }

        public string Digest { get; set; }

        public int NumberOfInstallments { get; set; }

        public bool Moto { get; set; }


    }
}
