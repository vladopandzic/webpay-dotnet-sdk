using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebPay.Request
{

    [XmlRoot("transaction")]
    public class Transaction
    {
        //Buyer
        [XmlElement("ch_full_name")]
        public string FullName { get; set; }

        [XmlElement("ch_address")]
        public string Address { get; set; }

        [XmlElement("ch_city")]
        public string City { get; set; }

        [XmlElement("ch_zip")]
        public string ZIP { get; set; }

        [XmlElement("ch_country")]
        public string Country { get; set; }

        [XmlElement("ch_phone")]
        public string Phone { get; set; }

        [XmlElement("ch_email")]
        public string Email { get; set; }

        //Card
        [XmlElement("pan")]
        public string Pan { get; set; }

        [XmlElement("cvv")]
        public int CVV { get; set; }

        [XmlElement("expiration_date")]
        public string ExpirationDate { get; set; }

        //Order
        [XmlElement("order_info")]
        public string OrderInfo { get; set; }

        [XmlElement("order_number")]
        public string OrderNumber { get; set; }

        [XmlElement("amount")]
        public double Amount { get; set; }

        [XmlElement("currency")]
        public Currency Currency { get; set; }

        //Processing data
        [XmlElement("ip")]
        public string Ip { get; set; }

        [XmlElement("language")]
        public Language Language { get; set; }

        [XmlElement("transaction_type")]
        public TransactionType TransactionType { get; set; }

        [XmlElement("authenticity_token")]
        public string AuthenticityToken { get; set; }

        [XmlElement("digest")]
        public string Digest { get; set; }

        [XmlElement("number_of_installments")]
        public int NumberOfInstallments { get; set; }

        [XmlElement("moto")]
        public bool Moto { get; set; }

    }
}
