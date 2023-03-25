using ColonizationMobileGame.BuildSystem;
using UnityEngine;

namespace ColonizationMobileGame.GameEvents
{
    public class ShelterBuiltEvent : MonoBehaviour
    {
        [SerializeField] private ShelterBuiltEventSO eventSO;
        [SerializeField] private Builder shelterBuilder;


        private void OnEnable()
        {
            shelterBuilder.Built += eventSO.Raise;
        }


        private void OnDisable()
        {
            shelterBuilder.Built -= eventSO.Raise;
        }
    }
}