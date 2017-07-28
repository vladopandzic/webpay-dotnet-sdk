using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebPay.Response
{
    public enum ReponseCode
    {
        [XmlEnum("0000")]
        Approved = 0000,
        [XmlEnum("1001")]
        CardExpired = 1001,
        [XmlEnum("1002")]
        CardSuspicious = 1002,
        [XmlEnum("1003")]
        CardSuspended = 1003,
        [XmlEnum("1004")]
        CardStolen = 1004,
        [XmlEnum("1005")]
        CardLost = 1005,
        [XmlEnum("1011")]
        CardNotFound = 1011,
        [XmlEnum("1012")]
        CardHolderNotFound = 1012,
        [XmlEnum("1014")]
        AccountNotFound = 1014,
        [XmlEnum("1015")]
        InvalidRequest = 1015,
        [XmlEnum("1016")]
        NotSufficientFunds = 1016,
        [XmlEnum("1017")]
        PreviouslyReversed = 1017,
        [XmlEnum("1018")]
        Previouslyreversed = 1018,
        [XmlEnum("1019")]
        FurtherActivityPreventsReversal = 1019,
        [XmlEnum("1020")]
        FurtherActivityPreventsVoid = 1020,
        [XmlEnum("1021")]
        OriginalTransactionHasBeenVoided = 1021,


        // ITD
//1022 - Preauthorization is not allowed for this card
//1023 - Only full 3D authentication is allowed for this card
//1024 - Installments are not allowed for this card
//1025 - Transaction with installments can not be send as preauthorization
//1026 - Installments are not allowed for non ZABA cards
//1050 - Transaction declined
//1802 - Missing fields
//1803 - Extra fields exist
//1804 - Invalid card number
//1806 - Card not active
//1808 - Card not configured
//1810 - Invalid amount
//1811 - System error - database
//1812 - System error - transaction
//1813 - Cardholder not active
//1814 - Cardholder not configured
//1815 - Cardholder expired
//1816 - Original not found
//1817 - Usage limit reached
//1818 - Configuration error
//1819 - Invalid terminal
//1820 - Inactive terminal
//1821 - Invalid merchant
//1822 - Duplicate entity
//1823 - Invalid acquirer
//2000 - Internal error - host down
//2001 - Internal error - host timeout
//2002 - Internal error - invalid message
//2003 - Internal error - message format error
//2013 - 3D Secure error - invalid request
//3000 - Time expired
//3100 - Function not supported
//3200 - Timeout
//3201 - Authorization host not active
//3202 - System not ready
//4001 - 3D Secure error - ECI 7
//4002 - 3D Secure error - not 3D Secure, store policy
//4003 - 3D secure error - not authenticated
//5000 - Request in progress
//5018 - RISK: Minimum amount per transaction
//5019 - RISK: Maximum amount per transaction
//5001 - RISK: Number of repeats per PAN
//5020 - RISK: Number of approved transactions per PAN
//5003 - RISK: Number of repeats per BIN
//5016 - RISK: Total sum on amount
//5021 - RISK: Sum on amount of approved transactions per PAN
//5022 - RISK: Sum on amount of approved transactions per BIN
//5005 - RISK: Percentage of declined transactions
//5009 - RISK: Number of chargebacks
//5010 - RISK: Sum on amount of chargebacks
//5006 - RISK: Number of refunded transactions
//5007 - RISK: Percentage increment of sum on amount of refunded transactions
//5023 - RISK: Number of approved transactions per PAN and MCC on amount
//5011 - RISK: Number of retrieval requests
//5012 - RISK: Sum on amount of retrieval requests
//5013 - RISK: Average amount per transaction
//5014 - RISK: Percentage increment of average amount per transaction
//5015 - RISK: Percentage increment of number of transactions
//5017 - RISK: Percentage increment of total sum on amount
//5050 - RISK: Number of repeats per IP
//5051 - RISK: Number of repeats per cardholder name
//5052 - RISK: Number of repeats per cardholder e-mail
//6000 - Systan mismatch
    }
}
