using System.Threading.Tasks;
using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;
using GenesisConstructa.ItemsPlacementsInteractions.Target;
using GenesisConstructa.Utility.Validating;

namespace GenesisConstructa.ItemsPlacementsInteractions.InteractionsSetup.Establisher
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