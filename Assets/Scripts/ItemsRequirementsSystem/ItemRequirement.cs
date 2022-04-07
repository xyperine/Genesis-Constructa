using System;
using UnityEngine;

namespace MoonPioneerClone.ItemsRequirementsSystem
{
    [Serializable]
    public sealed class ItemRequirement
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


#if UNITY_EDITOR
        public void UpdateCounter()
        {
            _currentAmount = 0;
        }
#endif
    }
}