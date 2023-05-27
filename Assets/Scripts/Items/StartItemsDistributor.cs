using System.Collections.Generic;
using System.Linq;
using GenesisConstructa.ItemsPlacementsInteractions;
using GenesisConstructa.Utility.Helpers;

namespace GenesisConstructa.Items
{
    public class StartItemsDistributor : ItemsDistributor<List<StackZoneItem>>
    {
        private readonly Dictionary<ItemType, int> _indicesForEachTypeOfItemsInZone = EnumHelpers.EnumToDictionary<ItemType, int>(-1);


        public StartItemsDistributor(List<StackZoneItem> itemsSource) : base(itemsSource)
        {
        }


        protected override StackZoneItem GetItem(ItemType itemType)
        {
            _indicesForEachTypeOfItemsInZone[itemType]++;
            int index = _indicesForEachTypeOfItemsInZone[itemType];

            StackZoneItem[] itemsOfType = itemsSource.Where(i => i.Type == itemType).ToArray();
            
            if (index >= itemsOfType.Length)
            {
                _indicesForEachTypeOfItemsInZone[itemType] = 0;
                index = _indicesForEachTypeOfItemsInZone[itemType];
            }

            return itemsOfType[index];
        }
    }
}