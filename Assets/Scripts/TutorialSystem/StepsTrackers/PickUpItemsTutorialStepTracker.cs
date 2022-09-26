using UnityEngine;

namespace ColonizationMobileGame.TutorialSystem.StepsTrackers
{
    public class PickUpItemsTutorialStepTracker : TutorialStepTracker
    {
        [SerializeField] private Transform itemsParent;


        private void Update()
        {
            if (itemsParent.childCount == 0)
            {
                InvokeCompleted();
            }
        }
    }
}