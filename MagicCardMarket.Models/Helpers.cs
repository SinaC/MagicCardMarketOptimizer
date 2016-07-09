using System;

namespace MagicCardMarket.Models
{
    public static class Helpers
    {
        public static int SafeConvertToInt(string s)
        {
            if (String.IsNullOrWhiteSpace(s))
                return 0;
            int result;
            return !int.TryParse(s, out result) ? 0 : result;
        }

        public static bool SafeConvertToBool(string s)
        {
            if (String.IsNullOrWhiteSpace(s))
                return false;
            s = s.ToLowerInvariant();
            return s == "1" || s == "true";
        }
    }
}
