using MoonPioneerClone.NewItemsInteractions.Transfer;
using UnityEngine;

namespace MoonPioneerClone.NewItemsInteractions
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