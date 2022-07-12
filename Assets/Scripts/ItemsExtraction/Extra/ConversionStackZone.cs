using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.UI;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.Extra
{
    public class ConversionStackZone : StackZone
    {
        [SerializeField] private ItemsCountPanelData itemsCountPanelData;


        private void Start()
        {
            SetStateObjectData();
        }


        public override void Add(StackZoneItem item)
        {
            base.Add(item);
            
            SetStateObjectData();
        }


        public override void Remove(StackZoneItem item)
        {
            base.Remove(item);
            
            SetStateObjectData();
        }


        private void SetStateObjectData()
        {
            itemsCountPanelData.SetData(new ItemCount(AcceptableItems.SingleOrDefault(), placement.Count, placement.Capacity));
        }
    }
}