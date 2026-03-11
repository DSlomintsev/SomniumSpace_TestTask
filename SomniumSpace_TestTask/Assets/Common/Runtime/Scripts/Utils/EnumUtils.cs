using System;
using System.Collections.Generic;


namespace Common.Utils
{
    public static class EnumUtils
    {
        public static Dictionary<string, T> GetEnumDict<T>(Dictionary<string, T> dict) where T : Enum
        {
            if (dict.Count == 0)
            {
                var enumValues = (T[])Enum.GetValues(typeof(T));
                for (var i = 0; i < enumValues.Length; i++)
                {
                    var enumValue = enumValues[i];
                    dict.Add(enumValue.ToString(), enumValue);
                }
            }

            return dict;
        }
    }
}