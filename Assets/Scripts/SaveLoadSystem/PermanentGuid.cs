using System;
using UnityEngine;

namespace ColonizationMobileGame.SaveLoadSystem
{
    [Serializable]
    public class PermanentGuid
    {
        [SerializeField] private string value;

        public string Value => value;


        public void Set(string newGuid)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = newGuid;
            }
        }
    }
}