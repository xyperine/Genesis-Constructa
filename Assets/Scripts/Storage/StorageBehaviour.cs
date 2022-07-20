using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacement.Core;
using ColonizationMobileGame.ItemsPlacement.Movers;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using UnityEngine;

namespace ColonizationMobileGame.Storage
{
    public class StorageBehaviour : InteractionTarget, IItemsAmountDataProvider
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;

        private Dictionary<ItemType, int> _itemsCount;
        private readonly DestroyingPlacementItemsMover _mover = new DestroyingPlacementItemsMover();

        public override bool CanTakeMore => true;
        public override ItemType[] AcceptableItems { get; } = Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToArray();


        private void Awake()
        {
            _itemsCount = AcceptableItems.ToDictionary(t => t, _ => 0);
            
            SetItemsAmountData();
        }


        public override void Add(StackZoneItem item)
        {
            _itemsCount[item.Type]++;
            
            item.SetFree();
            _mover.MoveItem(item.GetComponent<PlacementItem>(), transform.position);

            SetItemsAmountData();
        }


        public void SetItemsAmountData()
        {
            InItemAmountData[] data = _itemsCount.Where(ic => ic.Value > 0)
                .Select(ic => new InItemAmountData(ic.Key, ic.Value))
                .ToArray();
            itemsAmountPanelData.SetData(data);
            
            itemsAmountPanelData.InvokeChanged();
        }
    }
}