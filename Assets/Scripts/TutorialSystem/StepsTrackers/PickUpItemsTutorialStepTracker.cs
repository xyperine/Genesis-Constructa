using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.Utility.Extensions;
using UnityEngine;

namespace ColonizationMobileGame.TutorialSystem.StepsTrackers
{
    public class PickUpItemsTutorialStepTracker : TutorialStepTracker
    {
        [SerializeField] private Transform itemsParent;

        private Transform[] _items;


        private void Start()
        {
            _items = itemsParent.GetComponentsInChildren<StackZoneItem>().Select(i => i.transform).ToArray();
        }


        private void Update()
        {
            if (_items.IsNullOrEmpty())
            {
                InvokeCompleted();
                itemsParent.gameObject.SetActive(false);
                
                return;
            }
            
            if (_items.All(t => !t || t.parent != itemsParent))
            {
                InvokeCompleted();
                itemsParent.gameObject.SetActive(false);
            }
        }
    }
}