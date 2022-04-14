using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using MoonPioneerClone.ItemsPlacementsInteractions.Transfer;
using UnityEngine;

namespace MoonPioneerClone.Player
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