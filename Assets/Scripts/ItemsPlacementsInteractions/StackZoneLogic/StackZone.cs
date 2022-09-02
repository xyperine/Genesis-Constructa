using System;
using System.Linq;
using ColonizationMobileGame.ItemsPlacement.Core;
using ColonizationMobileGame.ItemsPlacement.Core.Area;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.UpgradingSystem;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic
{
    public class StackZone : InteractionTarget, IUpgradeable<StackZoneUpgradeData>, ISaveableWithGuid
    {
        [SerializeField] private ItemType[] acceptableItems;

        [SerializeField, HideInInspector] private PermanentGuid guid;

        protected PlacementArea placement;
        
        public bool HasItems => placement.Count > 0;
        public override bool CanTakeMore => placement.CanFitMore;
        public override ItemType[] AcceptableItems => (ItemType[]) acceptableItems.Clone();

        public SaveableType SaveableType => SaveableType.Other;
        public PermanentGuid Guid => guid;


        private void Awake()
        {
            placement = GetComponent<PlacementArea>();
        }


        private void OnValidate()
        {
            if (!TryGetComponent(out placement))
            {
                Debug.LogError("No PlacementArea component attached!");
            }
        }


        public void Setup(ItemType[] acceptableItems)
        {
            this.acceptableItems = acceptableItems;
        }


        public override void Add(StackZoneItem item)
        {
            if (!CanTakeThisItem(item))
            {
                return;
            }

            item.SetFree();
            item.SetZone(this);
            
            placement.Place(item.GetComponent<PlacementItem>());
        }
        
        
        private bool CanTakeThisItem(StackZoneItem item)
        {
            return acceptableItems.Contains(item.Type) && CanTakeMore;
        }


        public virtual void Remove(StackZoneItem item)
        {
            item.SetZone(null);
            placement.Remove(item.GetComponent<PlacementItem>());
        }


        public StackZoneItem GetLast(ItemType[] requestedItems)
        {
            if (requestedItems == null)
            {
                return null;
            }
            
            PlacementItem placementItem = placement.GetLast(requestedItems);

            if (!placementItem)
            {
                return null;
            }
            
            StackZoneItem item;
            placementItem.TryGetComponent(out item);

            return item;
        }


        public void Upgrade(StackZoneUpgradeData data)
        {
            placement.Upgrade(data.Capacity);
        }


        public object Save()
        {
            return new SaveData
            {
       
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;
        }
        
        
        [Serializable]
        private struct SaveData
        {
            public object PlacementAreaSaveData { get; set; }
        }
    }
}