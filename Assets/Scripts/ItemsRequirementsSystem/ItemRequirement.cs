using System;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.Utility.Validating;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.ItemsRequirementsSystem
{
    [Serializable]
    public sealed class ItemRequirement : IValidatable, ICloneable, ISaveable
    {
        [SerializeField] private ItemType type;
        [SerializeField, MinValue(1)] private int amount;

        public ItemType Type => type;

        public int CurrentAmount { get; private set; }

        public int Required => amount;

        public bool Fulfilled => CurrentAmount >= amount;
        

        public void AddOneItem()
        {
            CurrentAmount++;
        }

        
        public void OnValidate()
        {
            CurrentAmount = 0;
        }


        public object Clone()
        {
            ItemRequirement copy = new ItemRequirement
            {
                type = type,
                amount = amount,
            };

            return copy;
        }


        public object Save()
        {
            return new SaveData
            {
                CurrentAmount = CurrentAmount,
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            CurrentAmount = saveData.CurrentAmount;
        }
        
        
        [Serializable]
        private struct SaveData
        {
            public int CurrentAmount { get; set; }
        }
    }
}