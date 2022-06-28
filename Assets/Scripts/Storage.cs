using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacement.Core;
using ColonizationMobileGame.ItemsPlacement.Movers;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;

namespace ColonizationMobileGame
{
    public class Storage : InteractionTarget
    {
        private Dictionary<ItemType, int> _itemsCount;
        private readonly DestroyingPlacementItemsMover _mover = new DestroyingPlacementItemsMover();

        public override bool CanTakeMore => true;
        public override ItemType[] AcceptableItems { get; } = Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToArray();


        private void Awake()
        {
            _itemsCount = AcceptableItems.ToDictionary(t => t, _ => 0);
        }


        public override void Add(StackZoneItem item)
        {
            _itemsCount[item.Type]++;
            
            item.SetFree();
            _mover.MoveItem(item.GetComponent<PlacementItem>(), transform.position);
        }
    }
}