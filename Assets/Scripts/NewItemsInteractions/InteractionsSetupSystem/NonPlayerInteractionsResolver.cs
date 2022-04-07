using MoonPioneerClone.NewItemsInteractions.StackZoneLogic;
using MoonPioneerClone.NewItemsInteractions.Transfer.Target;
using MoonPioneerClone.Utility.Validating;

namespace MoonPioneerClone.NewItemsInteractions.InteractionsSetupSystem
{
    public class NonPlayerInteractionsResolver : InteractionsResolver<InteractionsList>
    {
        private readonly Validator _validator = new Validator();


        private void OnValidate()
        {
            _validator.Validate(this);
        }


        public override bool CanTransferTo(TransferTarget target)
        {
            return interactions.Exists(target, StackZoneInteractionType.Transfer);
        }


        public override bool CanPickUpFrom(StackZone zone)
        {
            return interactions.Exists(zone, StackZoneInteractionType.PickUp);
        }
    }
}