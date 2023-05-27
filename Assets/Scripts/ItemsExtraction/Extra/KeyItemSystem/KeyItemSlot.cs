using System;
using GenesisConstructa.ItemsPlacementsInteractions;
using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;
using GenesisConstructa.SaveLoadSystem;
using GenesisConstructa.UI.ItemsAmount.Data;
using UnityEngine;

namespace GenesisConstructa.ItemsExtraction.Extra.KeyItemSystem
{
    public class KeyItemSlot : StackZone, IItemsAmountDataProvider, ISaveable
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;

        private KeyItem _item;
        private object _itemSaveData;
        
        public bool Filled => HasItems;
        public float Lifetime => _item.Lifetime;


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


        public void Adjust(float l)
        {
            if (!_item)
            {
                return;
            }
            
            _item.Adjust(l);
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