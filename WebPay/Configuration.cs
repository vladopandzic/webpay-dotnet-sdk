using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPay
{
    public class Configuration
    {
        public string AuthenticityToken { get; set; }

        public string WebPayRootUrl { get; set; }

        public string Key { get; set; }
    }
}
