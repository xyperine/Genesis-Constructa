using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ColonizationMobileGame.Utility;
using UnityEngine;

namespace ColonizationMobileGame
{
    public sealed class LevelData : MonoBehaviour
    {
        private readonly ISingletonBehaviour<LevelData> _singletonBehaviour =
            new LocalizedSingletonMonoBehaviour<LevelData>();

        private readonly List<Structure> _structures = new List<Structure>();
        private readonly Dictionary<ItemType, uint> _itemsInStorage = Helpers.EnumToDictionary<ItemType, uint>(0);

        public IReadOnlyList<Structure> Structures => _structures;
        public ReadOnlyDictionary<ItemType, uint> ItemsInStorage => new ReadOnlyDictionary<ItemType, uint>(_itemsInStorage);

        public event Action Changed;


        private void Awake()
        {
            _singletonBehaviour.SetInstance(this);
        }


        public void SetStructure(Structure structure)
        {
            if (structure == null)
            {
                return;
            }
            
            _structures.Add(structure);
            
            InvokeChanged();
        }


        public void SetItemInStorage(ItemType item, uint count)
        {
            if (!_itemsInStorage.ContainsKey(item))
            {
                throw new KeyNotFoundException();
            }

            _itemsInStorage[item] = count;

            InvokeChanged();
        }


        private void InvokeChanged()
        {
            Changed?.Invoke();
        }
    }
}