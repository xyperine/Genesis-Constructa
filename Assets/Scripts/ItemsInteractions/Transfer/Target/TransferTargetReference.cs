using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.Transfer.Target
{
    public sealed class TransferTargetReference : MonoBehaviour
    {
        [SerializeField] private TransferTarget target;

        public TransferTarget Target => target;
    }
}
