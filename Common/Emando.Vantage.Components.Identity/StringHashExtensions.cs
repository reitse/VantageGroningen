using System;
using System.Security.Cryptography;
using System.Text;

namespace Emando.Vantage.Components.Identity
{
    public static class StringHashExtensions
    {
        public static string ToSHA256Base64Hash(this string s)
        {
            var buffer = Encoding.UTF8.GetBytes(s);
            var hashAlgorithm = new SHA256CryptoServiceProvider();
            var hash = hashAlgorithm.ComputeHash(buffer);
            return Convert.ToBase64String(hash);
        }
    }
}