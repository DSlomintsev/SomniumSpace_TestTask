using System.Globalization;

namespace Common.Utils
{
    public static class StringExt
    {
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
        public static int ToInt(this string str) => int.Parse(str, provider: CultureInfo.InvariantCulture);
        public static float ToFloat(this string valueStr) => float.Parse(valueStr, provider: CultureInfo.InvariantCulture);
        public static bool ToBoolean(this string valueStr) => bool.Parse(valueStr);
    }
}