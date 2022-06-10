using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.Target
{
    public sealed class InteractionTargetReference : MonoBehaviour
    {
        [SerializeField] private InteractionTarget target;

        public InteractionTarget Target => target;


        public void Setup(InteractionTarget target)
        {
            this.target = target;
        }
    }
}
