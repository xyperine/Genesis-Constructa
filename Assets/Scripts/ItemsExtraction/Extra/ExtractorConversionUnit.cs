using System.Collections;
using ColonizationMobileGame.ItemsExtraction.ConditionsLogic;
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
        [SerializeField] private ExtractorConditionsUnit conditionsUnit;
        [SerializeField] private ExtractorProductionUnit productionUnit;
        
        private readonly DestroyingPlacementItemsMover _itemsMover = new DestroyingPlacementItemsMover();
        
        private bool _active;
        private bool _canTakeMore = true;
        private IEnumerator _conversionCoroutine;

        public override bool CanTakeMore => _active && _canTakeMore && productionUnit.CanProduce;

        public override ItemType[] AcceptableItems { get; } =
        {
            ItemType.Oil,
            ItemType.Stone,
            ItemType.Iron,
            ItemType.Diamond,
        };
        
        
        private void OnEnable()
        {
            _active = conditionsUnit.ProductionConditionsMet;
            
            conditionsUnit.ConditionsChanged += OnConditionsChanged;
        }


        private void OnConditionsChanged()
        {
            _active = conditionsUnit.ProductionConditionsMet;
        }

        
        private void OnDisable()
        {
            conditionsUnit.ConditionsChanged -= OnConditionsChanged;
        }
        
        
        public override void Add(StackZoneItem item)
        {
            _itemsMover.MoveItem(item.GetComponent<PlacementItem>(), transform.localPosition);
            StartCoroutine(ProduceItemWithDelayCoroutine(item));

            if (_conversionCoroutine != null)
            {
                return;
            }

            _conversionCoroutine = ConversionCoroutine(); 
            StartCoroutine(_conversionCoroutine);
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

            _canTakeMore = true;
            _conversionCoroutine = null;
        }
    }
}