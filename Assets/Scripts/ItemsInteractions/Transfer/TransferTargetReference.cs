using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.Transfer
{
    public sealed class TransferTargetReference : MonoBehaviour
    {
        [SerializeField] private SerializedTransferTarget target;

        public ITransferTarget Target => target;
    }
}
