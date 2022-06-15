using System.Collections;
using ColonizationMobileGame.ItemsPlacement.Core;
using ColonizationMobileGame.ItemsPlacement.Movers;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.Utility;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.Extra
{
    public class ExtractorConversionUnit : InteractionTarget
    {
        [SerializeField] private ExtractorProductionUnit productionUnit;
        
        private readonly DestroyingPlacementItemsMover _itemsMover = new DestroyingPlacementItemsMover();
        
        private bool _canTakeMore = true;
        
        public override bool CanTakeMore => _canTakeMore;

        public override ItemType[] AcceptableItems { get; } =
        {
            ItemType.Oil,
            ItemType.Stone,
            ItemType.Iron,
            ItemType.Diamond,
        };
        
        
        public override void Add(StackZoneItem item)
        {
            _itemsMover.MoveItem(item.GetComponent<PlacementItem>(), transform.localPosition);
            StartCoroutine(AddRoutine());
        }


        private IEnumerator AddRoutine()
        {
            _canTakeMore = false;

            yield return Helpers.GetWaitForSeconds(1f / productionUnit.ItemsPerSecond);

            _canTakeMore = true;
        }
    }
}