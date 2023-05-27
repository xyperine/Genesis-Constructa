using System;
using System.Collections;
using GenesisConstructa.Items;
using GenesisConstructa.ItemsExtraction.ConditionsLogic;
using GenesisConstructa.ItemsExtraction.Upgrading;
using GenesisConstructa.ItemsPlacementsInteractions;
using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;
using GenesisConstructa.ObjectPooling;
using GenesisConstructa.SaveLoadSystem;
using GenesisConstructa.UpgradingSystem;
using UnityEngine;

namespace GenesisConstructa.ItemsExtraction
{
    public class ExtractorProductionUnit : MonoBehaviour, IUpgradeable<ExtractorUpgradeData>, ISceneSaveable
    {
        [SerializeField] private ExtractorConditionsUnit conditionsUnit;
        
        [SerializeField] private ExtractorProductionRateSO productionRateSO;
        [SerializeField] private ItemType itemType;

        [SerializeField] private StackZone productionStackZone;
        [SerializeField] private Conveyor conveyor;
        [SerializeField] private ItemsPool itemsPool;

        [SerializeField] private bool produce = true;
        [SerializeField] private ExtractorProductionWorkflow workflow;

        [SerializeField, HideInInspector] private PermanentGuid guid;

        private IEnumerator _productionCoroutine;
        private float _progress;

        public float ItemsPerSecond { get; private set; }

        public bool CanProduce => productionStackZone.CanTakeMore;

        public PermanentGuid Guid => guid;
        public int LoadingOrder => 0;


        private void Awake()
        {
            ItemsPerSecond = productionRateSO.ItemsPerSecond;
        }


        private void OnEnable()
        {
            if (workflow == ExtractorProductionWorkflow.Conversion)
            {
                return;
            }

            if (conditionsUnit.ProductionConditionsMet)
            {
                StartProduction();
            }
            
            conditionsUnit.ConditionsChanged += OnConditionsChanged;
        }


        private void StartProduction()
        {
            if (!produce)
            {
                return;
            }
            
            if (_productionCoroutine != null)
            {
                return;
            }

            _productionCoroutine = ProductionCoroutine();
            StartCoroutine(_productionCoroutine);
        }


        private IEnumerator ProductionCoroutine()
        {
            while (true)
            {
                _progress += Time.deltaTime * ItemsPerSecond;

                if (_progress >= 1f)
                {
                    ProduceItem();

                    _progress = 0f;
                }
                
                yield return null;
            }
            // ReSharper disable once IteratorNeverReturns
        }


        public void ProduceItem()
        {
            StackZoneItem item = itemsPool.Get(itemType, transform.position);
            conveyor.Add(item);
        }


        private void OnConditionsChanged()
        {
            if (conditionsUnit.ProductionConditionsMet)
            {
                StartProduction();
                return;
            }

            StopProduction();
        }


        private void StopProduction()
        {
            if (workflow == ExtractorProductionWorkflow.Conversion)
            {
                return;
            }
            
            if (_productionCoroutine == null)
            {
                return;
            }

            StopCoroutine(_productionCoroutine);
            _productionCoroutine = null;
        }


        private void OnDisable()
        {
            conditionsUnit.ConditionsChanged -= OnConditionsChanged;
        }


        public void SetPool(ItemsPool pool)
        {
            itemsPool = pool;
        }


        public void Upgrade(ExtractorUpgradeData data)
        {
            ItemsPerSecond = data.ItemsPerSecond;
        }


        public object Save()
        {
            return new SaveData
            {
                Progress = _progress,
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            _progress = saveData.Progress;
        }


        [Serializable]
        private struct SaveData
        {
            public float Progress { get; set; }
        }
    }
}