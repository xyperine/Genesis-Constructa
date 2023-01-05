using System;
using System.Collections.Generic;
using System.Linq;

namespace ColonizationMobileGame.Utility.Helpers
{
    public static class EnumHelpers
    {
        public static Dictionary<TEnum, TValue> EnumToDictionary<TEnum, TValue>(TValue[] values)
            where TEnum : Enum
        {
            Dictionary<TEnum, TValue> dictionary = new Dictionary<TEnum, TValue>();
            TEnum[] enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToArray();
            for (int i = 0; i < enumValues.Length; i++)
            {
                dictionary.Add(enumValues[i], values[i]);
            }

            return dictionary;
        }
        
        
        public static Dictionary<TEnum, TValue> EnumToDictionary<TEnum, TValue>(TValue valueForAll)
            where TEnum : Enum
        {
            TValue[] values = Enumerable.Repeat(valueForAll, Enum.GetValues(typeof(TEnum)).Length).ToArray();
            return EnumToDictionary<TEnum, TValue>(values);
        }
    }
}