using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.Transfer.Target
{
    public sealed class TransferTargetReference : MonoBehaviour
    {
        [SerializeField] private TransferTarget target;

        public TransferTarget Target => target;
    }
}
