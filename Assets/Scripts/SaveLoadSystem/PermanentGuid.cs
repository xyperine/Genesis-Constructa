﻿using System;
using UnityEngine;

namespace ColonizationMobileGame.SaveLoadSystem
{
    [Serializable]
    public class PermanentGuid
    {
        [SerializeField] private string value;

        // Need public setter in order to be serialized from json
        public string Value
        {
            get => value;
            set => Set(value);
        }


        public void Set(string newGuid)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = newGuid;
            }
        }
    }
}