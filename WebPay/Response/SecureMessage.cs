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
        public int Id { get; set; }

        [XmlElement("acs-url")]
        public int AcsUrl { get; set; }

        [XmlElement("pareq")]
        public int Pareq { get; set; }

        [XmlElement("authenticity-token")]
        public int AuthenticityToken { get; set; }
    }
}
