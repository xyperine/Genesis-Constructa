﻿using GenesisConstructa.Items;
using GenesisConstructa.ItemsPlacementsInteractions;
using UnityEngine;

namespace GenesisConstructa.ObjectPooling
{
    public class PoolItemsDistributor : ItemsDistributor<ItemsPool>
    {
        public PoolItemsDistributor(ItemsPool itemsSource) : base(itemsSource)
        {
        }


        protected override StackZoneItem GetItem(ItemType itemType)
        {
            return itemsSource.Get(itemType, Vector3.zero);
        }
    }
}