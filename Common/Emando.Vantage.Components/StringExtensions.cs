using System.Runtime.InteropServices;
using System.Security;

namespace Emando.Vantage.Components
{
    public static class StringExtensions
    {
        public static SecureString ToSecureString(this string unsecureString)
        {
            if (unsecureString == null)
                return null;

            var secureString = new SecureString();
            foreach (var c in unsecureString)
                secureString.AppendChar(c);
            return secureString;
        }

        public static string ToUnsecureString(this SecureString secureString)
        {
            if (secureString == null)
                return null;

            var unsecureString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
            try
            {
                return Marshal.PtrToStringUni(unsecureString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unsecureString);
            }
        }
    }
}