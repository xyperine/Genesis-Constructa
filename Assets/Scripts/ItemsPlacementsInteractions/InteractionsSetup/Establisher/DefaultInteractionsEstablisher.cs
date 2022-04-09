using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using MoonPioneerClone.Utility.Validating;

namespace MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup.Establisher
{
    public class DefaultInteractionsEstablisher : InteractionsEstablisher<InteractionsList>
    {
        private readonly Validator _validator = new Validator();


        private void OnValidate()
        {
            _validator.Validate(this);
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