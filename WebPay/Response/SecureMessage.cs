using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebPay.Response
{
    [XmlRoot("secure-message")]
    public class SecureMessage
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("acs-url")]
        public string AcsUrl { get; set; }

        [XmlElement("pareq")]
        public string Pareq { get; set; }

        [XmlElement("authenticity-token")]
        public string AuthenticityToken { get; set; }
    }
}
