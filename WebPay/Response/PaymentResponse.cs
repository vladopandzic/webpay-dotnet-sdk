using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebPay.Request;

namespace WebPay.Response
{
    [XmlRoot("transaction")]
    public class PaymentResponse
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("acquirer")]
        public string Acquirer { get; set; }

        [XmlElement("order-number")]
        public int OrderNumber { get; set; }

        [XmlElement("amount")]
        public long Amount { get; set; }

        [XmlElement("approval-code")]
        public string ApprovalCode { get; set; }

        [XmlElement("response-message")]
        public string ResponseMessage { get; set; }

        [XmlElement("reference-number")]
        public string ReferenceNumber { get; set; }

        [XmlElement("systan")]
        public long Systan { get; set; }

        [XmlElement("eci")]
        public long Eci { get; set; }

        [XmlElement("xid")]
        public string Xid { get; set; }

        [XmlElement("acsv")]
        public string Acsv { get; set; }

        [XmlElement("cc-type")]
        public string Cctype { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("transaction-type")]
        public TransactionType TransactionType { get; set; }

        [XmlElement("created-at")]
        public DateTime CreatedAt { get; set; }

        [XmlElement("enrollment")]
        public string Enrollment { get; set; }

        [XmlElement("authentication")]
        public string Authentication { get; set; }

    }
}
