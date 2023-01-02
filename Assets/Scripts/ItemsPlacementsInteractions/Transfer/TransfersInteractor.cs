using ColonizationMobileGame.ItemsPlacementsInteractions.InteractionsSetup.Establisher;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.Transfer
{
    public class TransfersInteractor : StackZoneInteractor<InteractionTargetReference>
    {
        [SerializeField] private TransferStackZoneBehaviour transferBehaviour;

        protected override bool Valid => transferBehaviour.CanGive;

        
        public void Setup(InteractionsEstablisher establisher, TransferStackZoneBehaviour transferBehaviour)
        {
            this.establisher = establisher;
            this.transferBehaviour = transferBehaviour;
        }


        protected override void InteractWith(int objID)
        {
            if (!Interactables.IDsToObjectsMap.TryGetValue(objID, out MonoBehaviour value))
            {
                return;
            }

            if (value is not InteractionTargetReference)
            {
                return;
            }
            
            InteractionTargetReference reference = (InteractionTargetReference) Interactables.IDsToObjectsMap[objID];

            if (!establisher.CanTransferTo(reference.Target))
            {
                return;
            }
            
            transferBehaviour.TransferTo(reference.Target);
        }
    }
}