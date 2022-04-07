using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Component = UnityEngine.Component;

namespace MoonPioneerClone.Utility.Validating
{
    public class Validator
    {
        public void Validate(Component component)
        {
            ValidateFields(component);
        }


        private void ValidateFields(object obj)
        {
            FieldInfo[] fields = GetFields(obj.GetType());

            if (!fields.Any())
            {
                return;
            }
            
            foreach (FieldInfo field in fields)
            {
                ValidateField(field, obj);

                ValidateFields(field.GetValue(obj));

                if (field.GetValue(obj) is IEnumerable collection)
                {
                    ValidateCollection(collection);
                }
            }
        }


        private FieldInfo[] GetFields(Type type)
        {
            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
            FieldInfo[] fields = type.GetFields(bindingFlags);

            fields = GetFilteredFields(fields);

            return fields;
        }


        private FieldInfo[] GetFilteredFields(IEnumerable<FieldInfo> fields)
        {
            fields = fields.Where(f => f.FieldType != typeof(MonoBehaviour));
            fields = fields.Where(f => (f.FieldType.Attributes & TypeAttributes.Serializable) != 0);
            fields = fields.Where(f => f.CustomAttributes.Any(d => d.AttributeType == typeof(SerializeField)));

            return fields.ToArray();
        }


        private void ValidateCollection(IEnumerable collection)
        {
            foreach (object obj in collection)
            {
                ValidateObject(obj);
            }
        }


        private void ValidateField(FieldInfo field, object obj)
        {
            ValidateObject(field.GetValue(obj));
        }


        private void ValidateObject(object obj)
        {
            if (obj is IValidatable validatable)
            {
                validatable.OnValidate();
            }
        }
    }
}