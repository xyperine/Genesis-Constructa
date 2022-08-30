using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.Utility;
using UnityEngine;

namespace ColonizationMobileGame.Level
{
    public class StartItems : MonoBehaviour, ISaveableWithGuid
    {
        [SerializeField, HideInInspector] private PermanentGuid guid;

        private StartItemsDistributor _itemsDistributor;
        private List<StackZoneItem> _items;

        public PermanentGuid Guid => guid;
        
        
        private void Awake()
        {
            _items = GetComponentsInChildren<StackZoneItem>(true).ToList();
            _itemsDistributor = new StartItemsDistributor(_items);
        }


        public object Save()
        {
            _items.RemoveAll(i => i == null);

            return new SaveData
            {
                ItemsInStackZones = _items.MapItemsToZones(),
                Items = _items.Select(i => i.Type).ToArray(),
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;
            
            RestoreItems(saveData);
        }


        private void RestoreItems(SaveData saveData)
        {
            IEnumerable<ItemType> itemsToDelete = GetItemsToDelete(saveData.Items);
            DeleteItems(itemsToDelete);
            
            _itemsDistributor.Distribute(saveData.ItemsInStackZones);
        }


        private IEnumerable<ItemType> GetItemsToDelete(IEnumerable<ItemType> itemsToPreserve)
        {
            List<ItemType> itemsToDelete = _items.Select(i => i.Type).ToList();
            foreach (ItemType itemType in itemsToPreserve)
            {
                if (itemsToDelete.Contains(itemType))
                {
                    itemsToDelete.Remove(itemType);
                }
            }

            return itemsToDelete;
        }


        private void DeleteItems(IEnumerable<ItemType> itemsToDelete)
        {
            foreach (ItemType itemType in itemsToDelete)
            {
                StackZoneItem item = _items.FirstOrDefault(i => i.Type == itemType);
                
                if (!item)
                {
                    continue;
                }
                
                item.Return();
                _items.Remove(item);
            }
        }


        [Serializable]
        private struct SaveData
        {
            public Dictionary<string, ItemType[]> ItemsInStackZones { get; set; }
            public ItemType[] Items { get; set; }
        }
    }
}