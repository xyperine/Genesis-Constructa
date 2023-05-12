using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.Utility.Extensions;
using UnityEngine;

namespace ColonizationMobileGame.TutorialSystem.StepsTrackers
{
    public class PickUpItemsTutorialStepTracker : TutorialStepTracker
    {
        [SerializeField] private Transform itemsParent;

        private Transform[] _itemsTransforms;


        private void Start()
        {
            _itemsTransforms = itemsParent.GetComponentsInChildren<StackZoneItem>().Select(i => i.transform).ToArray();
        }


        private void Update()
        {
            if (_itemsTransforms.IsNullOrEmpty())
            {
                InvokeCompleted();
                itemsParent.gameObject.SetActive(false);
                
                return;
            }
            
            if (_itemsTransforms.All(t => !t || t.parent != itemsParent))
            {
                InvokeCompleted();
                itemsParent.gameObject.SetActive(false);
            }
        }
    }
}