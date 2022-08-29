using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using UnityEngine;

namespace ColonizationMobileGame.ObjectPooling
{
    public class ItemsDistributor
    {
        private readonly ItemsPool _pool;
        
        
        public ItemsDistributor(ItemsPool pool)
        {
            _pool = pool;
        }
        
        
        public void Distribute(Dictionary<string, ItemType[]> itemsInsideStackZones)
        {
            if (itemsInsideStackZones == null || !itemsInsideStackZones.Any())
            {
                return;
            }
            
            StackZone[] zones = Object.FindObjectsOfType<StackZone>();

            foreach (KeyValuePair<string, ItemType[]> itemsInsideStackZone in itemsInsideStackZones)
            {
                if (string.IsNullOrEmpty(itemsInsideStackZone.Key))
                {
                    continue;
                }
                
                StackZone zone = zones.SingleOrDefault(z => z.Guid.Value == itemsInsideStackZone.Key);
                
                if (zone)
                {
                    foreach (ItemType itemType in itemsInsideStackZone.Value)
                    {
                        zone.Add(_pool.Get(itemType, zone.transform.position));
                    }
                }
            }
        }
    }
}