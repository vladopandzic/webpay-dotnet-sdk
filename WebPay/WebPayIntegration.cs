using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPay
{
    public class WebPayIntegration
    {
        private Configuration ConfigurationSettings;

        public WebPayIntegration(Configuration configuration)
        {
            ConfigurationSettings = configuration;
           // ValidateConfiguration(configuration);
        }
    }
}
