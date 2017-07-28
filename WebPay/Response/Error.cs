using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebPay.Response
{
    public class ErrorElement
    {
        public ErrorElement(string Error)
        {
            this.Error = Error;
        }

      
        public string Error { get; set; }
    }
}
