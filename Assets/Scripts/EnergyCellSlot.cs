using MoonPioneerClone.ItemsPlacementsInteractions;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;

namespace MoonPioneerClone
{
    public class EnergyCellSlot : StackZone
    {
        public override void Add(StackZoneItem item)
        {
            base.Add(item);

            item.GetComponent<EnergyCell>().Activate();
        }
    }
}