using MoonPioneerClone.ItemsPlacementsInteractions.Transfer;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions
{
    public sealed class PlayerTransferBehaviour : TransferStackZoneBehaviour
    {
        [SerializeField] private Joystick joystick;


        protected override bool NeedToBrakeTransfer()
        {
            bool standingStill = joystick.Direction == Vector2.zero;
            return !standingStill;
        }
    }
}