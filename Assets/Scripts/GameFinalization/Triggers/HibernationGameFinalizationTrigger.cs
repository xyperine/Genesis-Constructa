using ColonizationMobileGame.Hibernation.Area;
using UnityEngine;

namespace ColonizationMobileGame.GameFinalization.Triggers
{
    public class HibernationGameFinalizationTrigger : GameFinalizationTrigger
    {
        [SerializeField] private HibernationArea hibernationArea;


        private void Awake()
        {
            hibernationArea.Hibernated += Trigger;
        }
    }
}