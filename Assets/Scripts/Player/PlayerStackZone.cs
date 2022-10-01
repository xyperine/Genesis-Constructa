using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using ColonizationMobileGame.Utility;
using UnityEngine;

namespace ColonizationMobileGame.Player
{
    public class PlayerStackZone : StackZone, IItemsAmountDataProvider
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;
        
        private readonly Dictionary<ItemType, int> _itemsCount = Helpers.EnumToDictionary<ItemType, int>(0);


        private void Start()
        {
            SetItemsAmountData();
        }


        public override void Add(StackZoneItem item)
        {
            base.Add(item);

            _itemsCount[item.Type]++;
            
            SetItemsAmountData();
        }


        public override void Remove(StackZoneItem item)
        {
            base.Remove(item);

            _itemsCount[item.Type]--;
            
            SetItemsAmountData();
        }


        public void SetItemsAmountData()
        {
            ItemAmountData[] data = _itemsCount.Select(kvp => new ItemAmountData(kvp.Key, kvp.Value)).ToArray();
            itemsAmountPanelData.SetData(data);

            itemsAmountPanelData.InvokeChanged();
        }
    }
}