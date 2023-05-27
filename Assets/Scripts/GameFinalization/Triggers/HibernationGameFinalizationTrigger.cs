using GenesisConstructa.Hibernation.Area;
using UnityEngine;

namespace GenesisConstructa.GameFinalization.Triggers
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