using ColonizationMobileGame.BuildSystem;
using UnityEngine;

namespace ColonizationMobileGame.GameFinalization.Triggers
{
    public class ShelterBuiltGameFinalizationTrigger : GameFinalizationTrigger
    {
        [SerializeField] private Builder shelterBuilder;


        private void Awake()
        {
            shelterBuilder.Built += Trigger;
        }
    }
}