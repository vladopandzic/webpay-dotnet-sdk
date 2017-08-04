using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebPay.Request;

namespace WebPay.Core
{
    public class RequestBuilder
    {
        public long MakeCleanLongIntFromDecimal(decimal amount)
        {
            return Convert.ToInt64(amount.ToString("F").Replace(",", "").Replace(".", ""));
        }

        public string CreateDigest(string key, string orderNumber, decimal amount, Currency currency)
        {
            var hashBase = string.Format("{0}{1}{2}{3}", key, orderNumber, MakeCleanLongIntFromDecimal(amount), currency.ToString());
            return CalculateHash(hashBase);
        }
        protected string CalculateHash(string _hash_base)
        {
            UTF8Encoding Utf8enc = new UTF8Encoding();
            Byte[] ByteSourceText = Utf8enc.GetBytes(_hash_base);
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();

            //Make hash
            Byte[] ByteHash = SHA1.ComputeHash(ByteSourceText);

            StringBuilder sb = new StringBuilder(64);

            for (int i = 0; i < ByteHash.Length; i++)
                sb.Append(String.Format("{0:x2}", ByteHash[i]));

            return sb.ToString();
        }
    }
}
