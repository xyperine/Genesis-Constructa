using System.Threading.Tasks;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.Utility.Validating;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.InteractionsSetup.Establisher
{
    public class DefaultInteractionsEstablisher : InteractionsEstablisher<InteractionsList>
    {
        private async void OnValidate()
        {
            while (interactions == null)
            {
                await Task.Yield();
            }

            Validator.Validate(this);
        }


        public override bool CanTransferTo(InteractionTarget target)
        {
            return interactions.Exists(target, InteractionType.Transfer);
        }


        public override bool CanPickUpFrom(StackZone zone)
        {
            return interactions.Exists(zone, InteractionType.PickUp);
        }
    }
}