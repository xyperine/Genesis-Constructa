using MoonPioneerClone.ItemsInteractions.Transfer;
using UnityEngine;

namespace MoonPioneerClone.Player
{
    public sealed class PlayerTransferBehaviour : TransferStackZoneBehaviour
    {
        [SerializeField] private Joystick joystick;


        protected override bool NeedToBrakeTransfer()
        {
            bool standingStill = joystick.Direction == Vector2.zero; // Check for movement state here
            return !standingStill;
        }
    }
}