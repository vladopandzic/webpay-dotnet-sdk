using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPay.Exceptions;

namespace WebPay
{
    public class WebPayIntegration
    {
        public Configuration ConfigurationSettings;

        public WebPayIntegration(Configuration configuration)
        {
           
            if (configuration == null) {
                throw new IntergrationException("Configuration can't be null");
            }
            if (configuration.AuthenticityToken.Length != 40) {
                throw new IntergrationException("Authenticity token must be have 40 characters");
            }
            ConfigurationSettings = configuration;
            // ValidateConfiguration(configuration);
        }
    }
}
