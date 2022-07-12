using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacement.Core;
using ColonizationMobileGame.ItemsPlacement.Movers;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.UI;
using UnityEngine;

namespace ColonizationMobileGame
{
    public class Storage : InteractionTarget
    {
        [SerializeField] private ItemsCountPanelData itemsCountPanelData;

        private Dictionary<ItemType, int> _itemsCount;
        private readonly DestroyingPlacementItemsMover _mover = new DestroyingPlacementItemsMover();

        public override bool CanTakeMore => true;
        public override ItemType[] AcceptableItems { get; } = Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToArray();


        private void Awake()
        {
            _itemsCount = AcceptableItems.ToDictionary(t => t, _ => 0);
            
            SetStateObjectData();
        }


        public override void Add(StackZoneItem item)
        {
            _itemsCount[item.Type]++;
            
            item.SetFree();
            _mover.MoveItem(item.GetComponent<PlacementItem>(), transform.position);

            SetStateObjectData();
        }


        private void SetStateObjectData()
        {
            ItemCount[] data = _itemsCount.Where(ic => ic.Value > 0)
                .Select(ic => new ItemCount(ic.Key, ic.Value))
                .ToArray();
            itemsCountPanelData.SetData(data, null);
        }
    }
}