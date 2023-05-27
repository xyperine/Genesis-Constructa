using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenesisConstructa.Utility
{
    public static class DeepComparer
    {
        public static List<string> Compare<T>(T obj1, T obj2)
        {
            List<string> changedProperties = new List<string>();

            PropertyInfo[] properties = GetProperties(typeof(T));

            if (obj1 == null || obj2 == null)
            {
                changedProperties.AddRange(properties.Select(property => property.Name));
            }

            changedProperties.AddRange(properties
                .Where(property => !Equals(property.GetValue(obj1), property.GetValue(obj2)))
                .Select(property => property.Name));

            return changedProperties;
        }


        private static PropertyInfo[] GetProperties(Type type)
        {
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            PropertyInfo[] properties = type.GetProperties(bindingFlags);
            
            return properties;
        }
    }
}