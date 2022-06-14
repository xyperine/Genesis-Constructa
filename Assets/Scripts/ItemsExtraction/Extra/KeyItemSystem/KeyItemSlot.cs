using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;

namespace ColonizationMobileGame.ItemsExtraction.Extra.KeyItemSystem
{
    public class KeyItemSlot : StackZone
    {
        private KeyItem _item;
        
        public bool Satisfied => HasItems;


        public override void Add(StackZoneItem item)
        {
            base.Add(item);

            _item = item.GetComponent<KeyItem>();
        }


        public void Tick()
        {
            _item.Tick();
        }
    }
}