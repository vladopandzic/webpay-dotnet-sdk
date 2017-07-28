using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebPay.Response
{

    [XmlRoot("errors")]
    public class ErrorsResponse
    {
        [XmlElement("error")]
        public List<string> Errors { get; set; }
    }
}
