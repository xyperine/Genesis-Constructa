using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.Utility;
using UnityEngine;

namespace ColonizationMobileGame.Level
{
    public class StartItems : MonoBehaviour, ISceneSaveable, IInteractablesTrackerUser
    {
        [SerializeField, HideInInspector] private PermanentGuid guid;

        private StartItemsDistributor _itemsDistributor;
        private List<StackZoneItem> _items;

        private InteractablesTracker _interactablesTracker;
        
        public int LoadingOrder => 10;
        public PermanentGuid Guid => guid;


        public void SetInteractablesTracker(InteractablesTracker interactablesTracker)
        {
            _interactablesTracker = interactablesTracker;

            RegisterItemsInInteractablesTracker();
        }


        private void RegisterItemsInInteractablesTracker()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _interactablesTracker.RegisterObject(_items[i]);
            }
        }


        private void Awake()
        {
            _items = GetComponentsInChildren<StackZoneItem>(true).ToList();
            _itemsDistributor = new StartItemsDistributor(_items);
        }


        private void Start()
        {
            if (!_interactablesTracker)
            {
                return;
            }
            
            RegisterItemsInInteractablesTracker();
        }


        public object Save()
        {
            _items.RemoveAll(i => i == null);

            return new SaveData
            {
                ItemsInStackZones = _items.MapItemsToZones(),
                AllItems = _items.Select(i => i.Type).ToArray(),
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;
            
            RestoreItems(saveData);
        }


        private void RestoreItems(SaveData saveData)
        {
            IEnumerable<ItemType> itemsToDelete = GetItemsToDelete(saveData.AllItems);
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
            public ItemType[] AllItems { get; set; }
        }
    }
}