using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;

namespace ColonizationMobileGame
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