using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.Target
{
    public sealed class InteractionTargetReference : MonoBehaviour
    {
        [SerializeField] private InteractionTarget target;

        public InteractionTarget Target => target;
    }
}
