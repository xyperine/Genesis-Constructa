using System;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.Extra.KeyItemSystem
{
    public class KeyItemSlot : StackZone, IItemsAmountDataProvider, ISaveable
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;
        
        private KeyItem _item;
        private object _itemSaveData;
        
        public bool Filled => HasItems;
        public bool WillBeEmpty => Filled && _item.WillBeExhausted;


        private void Start()
        {
            SetItemsAmountData();
        }


        public override void Add(StackZoneItem item)
        {
            base.Add(item);

            _item = item.GetComponent<KeyItem>();
            if (_itemSaveData != null)
            {
                _item.Load(_itemSaveData);
            }
            
            SetItemsAmountData();
        }


        public void Tick()
        {
            if (!_item)
            {
                return;
            }
            
            _item.Tick();

            if (_item.Exhausted)
            {
                Clear();
            }
        }


        private void Clear()
        {
            _item.Clear();
            
            StackZoneItem zoneItem = _item.GetComponent<StackZoneItem>();
            Remove(zoneItem);
            zoneItem.Return();
            
            _item = null;
            _itemSaveData = null;

            SetItemsAmountData();
        }


        public void SetItemsAmountData()
        {
            itemsAmountPanelData.SetData(new ItemAmountData(AcceptableItems[0], placement.Count, 1));
            
            itemsAmountPanelData.InvokeChanged();
        }


        public object Save()
        {
            if (!_item)
            {
                return null;
            }

            return new SaveData
            {
                ItemData = _item.Save(),
            };
        }


        public void Load(object data)
        {
            if (data == null)
            {
                return;
            }
            
            SaveData saveData = (SaveData) data;
            
            _itemSaveData = saveData.ItemData;
        }


        [Serializable]
        private struct SaveData
        {
            public object ItemData { get; set; }
        }
    }
}