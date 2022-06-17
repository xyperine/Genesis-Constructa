using System.Collections;
using ColonizationMobileGame.ItemsExtraction.ConditionsLogic;
using ColonizationMobileGame.ItemsPlacement.Core;
using ColonizationMobileGame.ItemsPlacement.Movers;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.Utility;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.New
{
    public class ExtractorConversionUnit : InteractionTarget
    {
        [SerializeField] private ExtractorConditionsUnit conditionsUnit;
        [SerializeField] private ExtractorProductionUnit productionUnit;
        
        private readonly DestroyingPlacementItemsMover _itemsMover = new DestroyingPlacementItemsMover();
        
        private bool _canTakeMore;
        
        public override bool CanTakeMore => _canTakeMore && productionUnit.CanProduce;

        public override ItemType[] AcceptableItems { get; } =
        {
            ItemType.Oil,
            ItemType.Stone,
            ItemType.Iron,
            ItemType.Diamond,
        };
        
        
        private void OnEnable()
        {
            _canTakeMore = conditionsUnit.ProductionConditionsMet;
            
            conditionsUnit.ConditionsChanged += OnConditionsChanged;
        }


        private void OnConditionsChanged()
        {
            if (conditionsUnit.ProductionConditionsMet)
            {
                _canTakeMore = conditionsUnit.ProductionConditionsMet;
            }
        }

        
        private void OnDisable()
        {
            conditionsUnit.ConditionsChanged -= OnConditionsChanged;
        }
        
        
        public override void Add(StackZoneItem item)
        {
            _itemsMover.MoveItem(item.GetComponent<PlacementItem>(), transform.localPosition);
            StartCoroutine(ProduceItemWithDelayCoroutine(item));
            StartCoroutine(ConversionCoroutine());
        }


        private IEnumerator ProduceItemWithDelayCoroutine(StackZoneItem item)
        {
            yield return new WaitWhile(() => item.Moving);
            
            productionUnit.ProduceItem();
        }


        private IEnumerator ConversionCoroutine()
        {
            _canTakeMore = false;

            yield return Helpers.GetWaitForSeconds(1f / productionUnit.ItemsPerSecond);

            _canTakeMore = conditionsUnit.ProductionConditionsMet;
        }
    }
}