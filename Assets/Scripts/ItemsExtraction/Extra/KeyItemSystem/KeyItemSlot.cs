using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;

namespace ColonizationMobileGame.ItemsExtraction.Extra.KeyItemSystem
{
    public class KeyItemSlot : StackZone
    {
        private KeyItem _item;
        
        public bool Filled => HasItems;


        public override void Add(StackZoneItem item)
        {
            base.Add(item);

            _item = item.GetComponent<KeyItem>();
        }


        public void Tick()
        {
            if (!_item)
            {
                return;
            }
            
            _item.Tick();

            if (_item.Exhausted)
            {
                Clear();
            }
        }


        private void Clear()
        {
            StackZoneItem zoneItem = _item.GetComponent<StackZoneItem>();
            Remove(zoneItem);
            zoneItem.Return();
            
            _item = null;
        }
    }
}