using System.Collections.Generic;
using System.Linq;
using GenesisConstructa.ItemsPlacementsInteractions;
using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;
using GenesisConstructa.Utility.Extensions;
using UnityEngine;

namespace GenesisConstructa.Items
{
    public abstract class ItemsDistributor<TItemsSource>
    {
        protected readonly TItemsSource itemsSource;

        private StackZone[] _zones;


        protected ItemsDistributor(TItemsSource itemsSource)
        {
            this.itemsSource = itemsSource;
        }
        
        
        public void Distribute(Dictionary<string, ItemType[]> itemsInsideStackZones)
        {
            if (itemsInsideStackZones.IsNullOrEmpty())
            {
                return;
            }
            
            _zones = Object.FindObjectsOfType<StackZone>();

            foreach (KeyValuePair<string, ItemType[]> itemsInZone in itemsInsideStackZones)
            {
                AddItemsToZone(itemsInZone);
            }
        }


        private void AddItemsToZone(KeyValuePair<string, ItemType[]> itemsInZone)
        {
            if (string.IsNullOrEmpty(itemsInZone.Key))
            {
                return;
            }
                
            StackZone zone = _zones.SingleOrDefault(z => z.Guid.Value == itemsInZone.Key);

            if (!zone)
            {
                return;
            }

            foreach (ItemType itemType in itemsInZone.Value)
            {
                zone.Add(GetItem(itemType));
            }
        }


        protected abstract StackZoneItem GetItem(ItemType itemType);
    }
}