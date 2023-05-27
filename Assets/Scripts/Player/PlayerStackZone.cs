using System.Collections.Generic;
using System.Linq;
using GenesisConstructa.Audio;
using GenesisConstructa.Items;
using GenesisConstructa.ItemsPlacementsInteractions;
using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;
using GenesisConstructa.UI.ItemsAmount.Data;
using GenesisConstructa.Utility.Helpers;
using UnityEngine;

namespace GenesisConstructa.Player
{
    public class PlayerStackZone : StackZone, IItemsAmountDataProvider
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;
        [SerializeField] private PlayerZoneSoundEffect soundEffect;
        
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
            
            soundEffect.Play();
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
            
            soundEffect.Play();
        }
    }
}