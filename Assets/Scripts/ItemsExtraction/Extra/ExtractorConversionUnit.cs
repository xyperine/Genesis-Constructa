﻿using System.Collections;
using GenesisConstructa.Items;
using GenesisConstructa.ItemsExtraction.ConditionsLogic;
using GenesisConstructa.ItemsPlacement.Core;
using GenesisConstructa.ItemsPlacement.Movers;
using GenesisConstructa.ItemsPlacementsInteractions;
using GenesisConstructa.ItemsPlacementsInteractions.Target;
using UnityEngine;

namespace GenesisConstructa.ItemsExtraction.Extra
{
    public class ExtractorConversionUnit : InteractionTarget
    {
        [SerializeField] private ExtractorConditionsUnit conditionsUnit;
        [SerializeField] private ExtractorProductionUnit productionUnit;
        
        private readonly DestroyingPlacementItemsMover _itemsMover = new DestroyingPlacementItemsMover();
        
        private bool _active;
        private bool _canTakeMore = true;
        private IEnumerator _conversionCoroutine;

        private float _progress;

        public override bool CanTakeMore => _active && _canTakeMore && productionUnit.CanProduce;

        public override ItemType[] AcceptableItems { get; } =
        {
            ItemType.Metal,
            ItemType.Mineral,
            ItemType.Carbon,
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
            item.SetFree();
            
            _itemsMover.MoveItem(item.GetComponent<PlacementItem>(), transform.position);
            if (_conversionCoroutine != null)
            {
                return;
            }

            _conversionCoroutine = ConversionCoroutine(item); 
            StartCoroutine(_conversionCoroutine);
        }


        private IEnumerator ConversionCoroutine(StackZoneItem item)
        {
            _canTakeMore = false;

            yield return new WaitWhile(() => item.Moving);

            while (_progress < 1f)
            {
                _progress += Time.deltaTime * productionUnit.ItemsPerSecond;
                
                if (!_active)
                {
                    yield return new WaitUntil(() => _active);
                }
                
                yield return null;
            }
            
            productionUnit.ProduceItem();
            _progress = 0f;

            _canTakeMore = true;
            _conversionCoroutine = null;
        }
    }
}