namespace Emando.Vantage.Components.Competitions
{
    internal static class Int32Extensions
    {
        public static int Pow(this int x, int pow)
        {
            var result = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    result *= x;
                x *= x;
                pow >>= 1;
            }
            return result;
        }
    }
}