using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.Items;
using ColonizationMobileGame.ItemsPlacement.Core;
using ColonizationMobileGame.ItemsPlacement.Movers;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using ColonizationMobileGame.Utility.Helpers;
using UnityEngine;

namespace ColonizationMobileGame.Storage
{
    public class StorageBehaviour : InteractionTarget, IItemsAmountDataProvider, ISceneSaveable
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;

        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        private readonly DestroyingPlacementItemsMover _mover = new DestroyingPlacementItemsMover();

        private Dictionary<ItemType, int> _itemsCount = EnumHelpers.EnumToDictionary<ItemType, int>(0);

        public override bool CanTakeMore => true;
        public override ItemType[] AcceptableItems => _itemsCount.Select(kvp => kvp.Key).ToArray();

        public int LoadingOrder => 2;
        public PermanentGuid Guid => guid;


        private void Awake()
        {
            SetItemsAmountData();
        }


        public void SetItemsAmountData()
        {
            ItemAmountData[] data = _itemsCount.Where(kvp => kvp.Value > 0)
                .Select(kvp => new ItemAmountData(kvp.Key, kvp.Value))
                .ToArray();
            itemsAmountPanelData.SetData(data);
            
            itemsAmountPanelData.InvokeChanged();
        }


        public override void Add(StackZoneItem item)
        {
            _itemsCount[item.Type]++;
            
            item.SetFree();
            _mover.MoveItem(item.GetComponent<PlacementItem>(), transform.position);

            SetItemsAmountData();
        }


        public object Save()
        {
            return new SaveData
            {
                ItemsCount = _itemsCount,
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            _itemsCount = saveData.ItemsCount;

            SetItemsAmountData();
        }


        [Serializable]
        private struct SaveData
        {
            public Dictionary<ItemType, int> ItemsCount { get; set; }
        }
    }
}