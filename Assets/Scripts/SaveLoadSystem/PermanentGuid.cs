using System;
using UnityEngine;

namespace GenesisConstructa.SaveLoadSystem
{
    [Serializable]
    public class PermanentGuid
    {
        [SerializeField] private string value;

        // Need public setter in order to be serialized from json
        public string Value
        {
            get => value;
            set => TrySet(value);
        }


        public void TrySet(string newGuid)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = newGuid;
            }
        }


        public static string NewGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}