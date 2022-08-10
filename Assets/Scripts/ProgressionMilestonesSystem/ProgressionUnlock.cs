using UnityEngine;

namespace ColonizationMobileGame.ProgressionMilestonesSystem
{
    public class ProgressionUnlock : MonoBehaviour
    {
        [SerializeField] private GameObject unlockable;
        [SerializeField] private ProgressionMilestoneType milestoneType;

        private ProgressionEvents _progressionEvents;


        private void Awake()
        {
            _progressionEvents = FindObjectOfType<ProgressionEvents>();
        }


        private void OnEnable()
        {
            if (!_progressionEvents)
            {
                return;
            }
            
            _progressionEvents.Subscribe(milestoneType, Unlock);
        }


        private void OnDisable()
        {
            if (!_progressionEvents)
            {
                return;
            }
        
            _progressionEvents.Unsubscribe(milestoneType, Unlock);
        }


        private void Unlock()
        {
            unlockable.SetActive(true);
            enabled = false;
        }
    }
}