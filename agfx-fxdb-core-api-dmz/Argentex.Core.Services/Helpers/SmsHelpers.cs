using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Argentex.Core.Service.Helpers
{
    public class SmsHelpers
    {
        /// <summary>
        /// Get the 4 digit validation code for two-factor authentication
        /// </summary>
        /// <returns>4 digit code</returns>
        public static string GenerateValidationCodeFor2FA()
        {
            var generator = new Random();
            var validationCode = generator.Next(0, 9999).ToString("D4");
            return validationCode;
        }

        /// <summary>
        /// Encrypted validation code
        /// </summary>
        /// <param name="stringToEncrypt">any string</param>
        /// <returns>SHA256 encrypted code</returns>
        public static string GetHash(string stringToEncrypt)
        {
            // this a hashed SHA256 string
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(stringToEncrypt));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
