using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebPay.Request
{
    [XmlRoot("secure-message")]
    public class SecureMessageRequest
    {
     
        [XmlElement("PaRes")]
        public string PaRes { get; set; }

        [XmlElement("MD")]
        public string MD { get; set; }
        
    }
}
