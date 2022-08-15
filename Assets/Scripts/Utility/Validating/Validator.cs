using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ColonizationMobileGame.Utility.Validating
{
    /// <summary>
    /// Use it in OnValidate() event function to call IValidatable.OnValidate on a non-monobehaviour members of an object using this class
    /// </summary>
    public static class Validator
    {
        public static void Validate(Object unityObj, [CallerMemberName] string callingMethodName = default)
        {
            if (callingMethodName != "OnValidate")
            {
                throw new InvalidOperationException("Is not designed to be used outside of the OnValidate method!");
            }
            
            ValidateFieldsOf(unityObj);
        }


        private static void ValidateFieldsOf(object obj)
        {
            if (obj == null)
            {
                return;
            }
            
            FieldInfo[] fields = GetFields(obj.GetType());

            if (!fields.Any())
            {
                return;
            }
            
            foreach (FieldInfo field in fields)
            {
                ValidateField(field, obj);

                ValidateFieldsOf(field.GetValue(obj));

                if (field.GetValue(obj) is IEnumerable collection)
                {
                    ValidateCollection(collection);
                }
            }
        }


        private static FieldInfo[] GetFields(Type type)
        {
            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
            FieldInfo[] fields = type.GetFields(bindingFlags);

            fields = GetFilteredFields(fields);

            return fields;
        }


        private static FieldInfo[] GetFilteredFields(IEnumerable<FieldInfo> fields)
        {
            fields = fields.Where(f => f.FieldType != typeof(MonoBehaviour));
            fields = fields.Where(f => (f.FieldType.Attributes & TypeAttributes.Serializable) != 0);
            fields = fields.Where(f => f.CustomAttributes.Any(d => d.AttributeType == typeof(SerializeField)));

            return fields.ToArray();
        }


        private static void ValidateField(FieldInfo field, object obj)
        {
            ValidateObject(field.GetValue(obj));
        }


        private static void ValidateCollection(IEnumerable collection)
        {
            foreach (object obj in collection)
            {
                ValidateFieldsOf(obj);
                ValidateObject(obj);
            }
        }


        private static void ValidateObject(object obj)
        {
            if (obj is IValidatable validatable)
            {
                validatable.OnValidate();
            }
        }
    }
}