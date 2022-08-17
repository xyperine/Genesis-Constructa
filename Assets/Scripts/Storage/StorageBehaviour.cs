﻿using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacement.Core;
using ColonizationMobileGame.ItemsPlacement.Movers;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.Level;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using UnityEngine;

namespace ColonizationMobileGame.Storage
{
    public class StorageBehaviour : InteractionTarget, IItemsAmountDataProvider, ILevelDataUser
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;

        private readonly DestroyingPlacementItemsMover _mover = new DestroyingPlacementItemsMover();
        
        private Dictionary<ItemType, int> _itemsCount;
        private LevelData _levelData;

        public override bool CanTakeMore => true;
        public override ItemType[] AcceptableItems { get; } = Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToArray();


        public void SetLevelData(LevelData levelData)
        {
            _levelData = levelData;
        }
        
        
        private void Awake()
        {
            _itemsCount = AcceptableItems.ToDictionary(t => t, _ => 0);
            
            SetItemsAmountData();
        }


        public void SetItemsAmountData()
        {
            ItemAmountData[] data = _itemsCount.Where(ic => ic.Value > 0)
                .Select(ic => new ItemAmountData(ic.Key, ic.Value))
                .ToArray();
            itemsAmountPanelData.SetData(data);
            
            itemsAmountPanelData.InvokeChanged();
        }


        public override void Add(StackZoneItem item)
        {
            _itemsCount[item.Type]++;
            
            item.SetFree();
            _mover.MoveItem(item.GetComponent<PlacementItem>(), transform.position);
            
            _levelData.SetItemInStorage(item.Type, (uint) _itemsCount[item.Type]);

            SetItemsAmountData();
        }
    }
}