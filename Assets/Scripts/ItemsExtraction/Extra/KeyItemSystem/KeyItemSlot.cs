using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.Extra.KeyItemSystem
{
    public class KeyItemSlot : StackZone, IItemsAmountDataProvider
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;
        
        private KeyItem _item;
        
        public bool Filled => HasItems;


        private void Start()
        {
            SetItemsAmountData();
        }


        public override void Add(StackZoneItem item)
        {
            base.Add(item);

            _item = item.GetComponent<KeyItem>();
            
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
            StackZoneItem zoneItem = _item.GetComponent<StackZoneItem>();
            Remove(zoneItem);
            zoneItem.Return();
            
            _item = null;

            SetItemsAmountData();
        }


        public void SetItemsAmountData()
        {
            itemsAmountPanelData.SetData(new LimitedItemAmountData(AcceptableItems[0], placement.Count, 1));
            
            itemsAmountPanelData.InvokeChanged();
        }
    }
}