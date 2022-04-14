using System;
using MoonPioneerClone.Utility.Validating;
using UnityEngine;

namespace MoonPioneerClone.ItemsRequirementsSystem
{
    [Serializable]
    public sealed class ItemRequirement : IValidatable
    {
        [SerializeField] private ItemType type;
        [SerializeField, Min(1)] private int amount;
        
        private int _currentAmount;

        public ItemType Type => type;
        public bool Satisfied => _currentAmount >= amount;
        

        public void AddOneItem()
        {
            _currentAmount++;
        }

        
        public void OnValidate()
        {
            _currentAmount = 0;
        }
    }
}