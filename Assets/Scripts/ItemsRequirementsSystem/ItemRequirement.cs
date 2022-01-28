using System;
using UnityEngine;

namespace MoonPioneerClone.ItemsRequirementsSystem
{
    [Serializable]
    public sealed class ItemRequirement
    {
        [SerializeField] private ItemType type;
        [SerializeField, Min(1)] private int amount;
        
        private int _leftToSatisfaction;

        public ItemType Type => type;
        public bool Satisfied => _leftToSatisfaction <= 0;
        

        public void AddOneItem()
        {
            _leftToSatisfaction--;
        }


#if UNITY_EDITOR
        public void UpdateCounter()
        {
            _leftToSatisfaction = amount;
        }
#endif
    }
}