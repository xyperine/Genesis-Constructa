using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacement.Core;
using ColonizationMobileGame.ItemsPlacement.Movers;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.Level;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.ScoreSystem;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using ColonizationMobileGame.Utility;
using UnityEngine;

namespace ColonizationMobileGame.Storage
{
    public class StorageBehaviour : InteractionTarget, IItemsAmountDataProvider, ILevelDataUser, ISceneSaveable
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;
        [SerializeField] private ScoreModifier scoreModifier;

        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        private readonly DestroyingPlacementItemsMover _mover = new DestroyingPlacementItemsMover();

        private Dictionary<ItemType, int> _itemsCount = Helpers.EnumToDictionary<ItemType, int>(0);
        private LevelData _levelData;

        public override bool CanTakeMore => true;
        public override ItemType[] AcceptableItems => _itemsCount.Select(kvp => kvp.Key).ToArray();

        public SaveableType SaveableType => SaveableType.Other;
        public PermanentGuid Guid => guid;


        public void SetLevelData(LevelData levelData)
        {
            _levelData = levelData;
        }


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
            
            _levelData.SetItemsInStorage(_itemsCount);
            
            scoreModifier.Add(item.Type);

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
            _levelData.SetItemsInStorage(_itemsCount);
            SetItemsAmountData();
        }


        [Serializable]
        private struct SaveData
        {
            public Dictionary<ItemType, int> ItemsCount { get; set; }
        }
    }
}