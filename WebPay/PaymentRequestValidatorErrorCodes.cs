using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPay
{
    public enum PaymentRequestValidatorErrorCodes
    {
        BuyerFullNameInvalidLength=20000,
        BuyerAddressLength = 20001,
        BuyerCityLength = 20002,
        BuyerZIPLength=20003,
        BuyerCountryLength=20004,
        BuyerPhoneLength=20005,
        BuyerEmailLength=20006,
        CardPanLength=20007,
        CardCVVLength=20008,
        CardExpirationDate=20009,
        OrderOrderInfoLength=20010,
        OrderOrderNumberLength=20011,
        OrderAmountLength=20012,
        ProcessingDataIpLength=20013,
        ProcessingDataNumberOfInstallmentsRange=20014,
        BuyerEmail = 20015,
        CardPan = 20016,
    }
}
