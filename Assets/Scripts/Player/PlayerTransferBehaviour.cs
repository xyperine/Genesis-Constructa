using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.ItemsPlacementsInteractions.Transfer;
using UnityEngine;

namespace ColonizationMobileGame.Player
{
    public sealed class PlayerTransferBehaviour : TransferStackZoneBehaviour
    {
        [SerializeField] private Joystick joystick;


        protected override bool NeedToBrakeTransfer(InteractionTarget target)
        {
            bool standingStill = joystick.Direction == Vector2.zero;
            return !standingStill || base.NeedToBrakeTransfer(target);
        }
    }
}