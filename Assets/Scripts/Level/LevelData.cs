using System;
using System.Collections.Generic;
using ColonizationMobileGame.Structures;
using ColonizationMobileGame.Utility;
using ColonizationMobileGame.Utility.Helpers;
using UnityEngine;

namespace ColonizationMobileGame.Level
{
    [DisallowMultipleComponent]
    public sealed class LevelData : MonoBehaviour
    {
        private readonly List<Structure> _structures = new List<Structure>();
        private Dictionary<ItemType, int> _itemsInStorage = EnumHelpers.EnumToDictionary<ItemType, int>(0);

        public IReadOnlyList<Structure> Structures => _structures;
        public IReadOnlyDictionary<ItemType, int> ItemsInStorage => _itemsInStorage;

        public event Action Changed;


        public void SetStructure(Structure structure)
        {
            if (structure == null)
            {
                return;
            }
            
            _structures.Add(structure);
            
            InvokeChanged();
        }


        public void SetItemsInStorage(Dictionary<ItemType, int> itemsInStorage)
        {
            _itemsInStorage = itemsInStorage;

            InvokeChanged();
        }


        private void InvokeChanged()
        {
            Changed?.Invoke();
        }
    }
}