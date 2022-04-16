using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MoonPioneerClone.Utility
{
    public static class DetailedComparer
    {
        public static List<string> Compare<T>(T v1, T v2)
        {
            List<string> props = new List<string>();

            
            
            return props;
        }
        
        
        private static FieldInfo[] GetFields(Type type)
        {
            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
            FieldInfo[] fields = type.GetFields(bindingFlags);

            fields = GetInspectorExposedFields(fields);

            return fields;
        }


        private static FieldInfo[] GetInspectorExposedFields(IEnumerable<FieldInfo> fields)
        {
            fields = fields.Where(f => f.FieldType != typeof(MonoBehaviour));
            fields = fields.Where(f => (f.FieldType.Attributes & TypeAttributes.Serializable) != 0);
            fields = fields.Where(f => f.CustomAttributes.Any(d => d.AttributeType == typeof(SerializeField)));

            return fields.ToArray();
        }
    }
}