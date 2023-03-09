using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.Items;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using ColonizationMobileGame.Utility.Helpers;
using UnityEngine;

namespace ColonizationMobileGame.Player
{
    public class PlayerStackZone : StackZone, IItemsAmountDataProvider
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;
        
        private readonly Dictionary<ItemType, int> _itemsCount = EnumHelpers.EnumToDictionary<ItemType, int>(0);


        private void Start()
        {
            SetItemsAmountData();
        }


        public override void Add(StackZoneItem item)
        {
            base.Add(item);

            ModifyItemsCount(item.Type);

            SetItemsAmountData();
        }


        private void ModifyItemsCount(ItemType itemType)
        {
            _itemsCount[itemType] = placement.Items.Count(i => i.GetComponent<StackZoneItem>().Type == itemType);
        }


        public void SetItemsAmountData()
        {
            ItemAmountData[] data = _itemsCount.Select(kvp => new ItemAmountData(kvp.Key, kvp.Value)).ToArray();
            itemsAmountPanelData.SetData(data);

            itemsAmountPanelData.InvokeChanged();
        }


        public override void Remove(StackZoneItem item)
        {
            base.Remove(item);

            ModifyItemsCount(item.Type);

            SetItemsAmountData();
        }
    }
}