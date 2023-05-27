using System.Collections.Generic;
using System.Linq;
using GenesisConstructa.Items;
using GenesisConstructa.ItemsPlacementsInteractions;

namespace GenesisConstructa.Utility.Extensions
{
    public static class ItemsPlacementExtensions
    {
        public static Dictionary<string, ItemType[]> MapItemsToZones(this IEnumerable<StackZoneItem> itemsCollection)
        {
            StackZoneItem[] itemsInZones = itemsCollection.Where(i => i.Zone).ToArray();
            string[] zonesGuids = itemsInZones.Select(i => i.Zone.Guid.Value).Distinct().ToArray();

            return zonesGuids.ToDictionary(g => g,
                g => itemsInZones.Where(i => i.Zone.Guid.Value == g).Select(i => i.Type).ToArray());
        }
    }
}