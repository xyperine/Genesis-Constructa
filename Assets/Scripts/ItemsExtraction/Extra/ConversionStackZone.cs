using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.Extra
{
    public class ConversionStackZone : StackZone, IItemsAmountDataProvider
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;


        private void Start()
        {
            SetItemsAmountData();
        }


        public override void Add(StackZoneItem item)
        {
            base.Add(item);
            
            SetItemsAmountData();
        }


        public override void Remove(StackZoneItem item)
        {
            base.Remove(item);
            
            SetItemsAmountData();
        }


        public void SetItemsAmountData()
        {
            itemsAmountPanelData.SetData(new ItemAmountData(AcceptableItems.SingleOrDefault(), placement.Count, placement.Capacity));
            
            itemsAmountPanelData.InvokeChanged();
        }
    }
}