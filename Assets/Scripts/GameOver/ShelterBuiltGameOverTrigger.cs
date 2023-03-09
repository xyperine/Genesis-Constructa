using ColonizationMobileGame.BuildSystem;
using UnityEngine;

namespace ColonizationMobileGame.GameOver
{
    public class ShelterBuiltGameOverTrigger : GameOverTrigger
    {
        [SerializeField] private Builder shelterBuilder;


        private void Start()
        {
            shelterBuilder.Built += Trigger;
        }
    }
}