using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using UnityEngine;

namespace ColonizationMobileGame.TutorialSystem.StepsTrackers
{
    public class PickUpItemsTutorialStepTracker : TutorialStepTracker
    {
        [SerializeField] private Transform itemsParent;

        private int _nonItemsChildrenCount;


        private void Start()
        {
            _nonItemsChildrenCount = transform.Cast<Transform>().Count(t => !t.GetComponent<StackZoneItem>());
        }


        private void Update()
        {
            if (transform.childCount <= _nonItemsChildrenCount)
            {
                InvokeCompleted();
                itemsParent.gameObject.SetActive(false);
            }
        }
    }
}