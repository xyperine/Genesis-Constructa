using System;
using ColonizationMobileGame.InteractablesTracking;
using ColonizationMobileGame.ItemsExtraction;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ObjectPooling;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.Structures;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using ColonizationMobileGame.UnlockingSystem;
using ColonizationMobileGame.Utility.Extensions;
using UnityEngine;

namespace ColonizationMobileGame.BuildSystem
{
    public sealed class Builder : MonoBehaviour, IItemsAmountDataProvider, ISceneSaveable, IInteractablesTrackerUser, IIdentifiable
    {
        [SerializeField] private BuildDataSO buildDataSO;
        [SerializeField] private Transform structuresParent;
        [SerializeField] private ItemsPool itemsPool;
        [SerializeField] private ItemsConsumer consumer;
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;

        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        private InteractablesTracker _interactablesTracker;

        private BuildData _buildData;

        private StructureRuntimeSaveResolver _structureSaveResolver;
        private string _structureGuid;
        private object _structureData;

        public int LoadingOrder => -100;
        public PermanentGuid Guid => guid;
        
        public StructureIdentifier Identifier => _buildData.Identifier;

        public StructureType StructureType => _buildData.Identifier.StructureType;

        public event Action Built;


        public void SetInteractablesTracker(InteractablesTracker interactablesTracker)
        {
            _interactablesTracker = interactablesTracker;
        }


        private void Awake()
        {
            _buildData = buildDataSO.Data;
            
            SetupPrice();
            SetItemsAmountData();

            consumer.transform.localPosition = GetStructureBounds().center.XZPlane();
            consumer.Consumed += SetItemsAmountData;

            if (!_buildData.Locked)
            {
                return;
            }

            _buildData.Unlocked += OnUnlocked;
            gameObject.SetActive(false);
        }


        private void SetupPrice()
        {
            consumer.Setup(_buildData.Price);
            _buildData.Price.Fulfilled += Build;
        }


        public void Build()
        {
            GameObject structureObject = Instantiate(_buildData.StructurePrefab, transform.position,
                Quaternion.identity, structuresParent);
            IInteractablesTrackerUser[] interactablesTrackerUsers =
                structureObject.GetComponentsInChildren<IInteractablesTrackerUser>(true);
            ExtractorProductionUnit productionUnit = structureObject.GetComponentInChildren<ExtractorProductionUnit>();

            if (structureObject.TryGetComponent(out Structure structure))
            {
                structure.Setup(_buildData.Identifier.StructureType, _buildData.MaxLevel);
            }

            if (!interactablesTrackerUsers.IsNullOrEmpty())
            {
                for (int i = 0; i < interactablesTrackerUsers.Length; i++)
                {
                    interactablesTrackerUsers[i]?.SetInteractablesTracker(_interactablesTracker);
                }
            }

            if (structureObject.TryGetComponent(out _structureSaveResolver))
            {
                RestoreStructure();
            }

            if (productionUnit)
            {
                productionUnit.SetPool(itemsPool);
            }
            
            Built?.Invoke();

            Invoke(nameof(Deactivate), 1f);
        }


        private void RestoreStructure()
        {
            _structureGuid = string.IsNullOrEmpty(_structureGuid) ? PermanentGuid.NewGuid() : _structureGuid;
            _structureSaveResolver.Guid.TrySet(_structureGuid);
            if (_structureData != null)
            {
                _structureSaveResolver.Load(_structureData);
            }
        }


        private void Deactivate()
        {
            _buildData.Price.Fulfilled -= Build;
            Built = null;
            
            gameObject.SetActive(false);
        }


        private void OnUnlocked()
        {
            gameObject.SetActive(true);
            
            SetItemsAmountData();
        }


        public void SetItemsAmountData()
        {
            itemsAmountPanelData.SetData(_buildData.Price.ToItemsAmount());
            itemsAmountPanelData.SetIdentifier(_buildData.Identifier);
            itemsAmountPanelData.SetUnlockable(_buildData);
            
            itemsAmountPanelData.InvokeChanged();
        }


        public Bounds GetStructureBounds()
        {
            return _buildData.StructurePrefab.GetBounds();
        }
        
        
        public Vector2 GetStructureArea()
        {
            return GetStructureBounds().size.XZPlaneVector2();
        }


        public object Save()
        {
            return new SaveData
            {
                StructureGuid = _structureGuid,
                StructureData = _structureSaveResolver ? _structureSaveResolver.Save() : null,
                BuildPriceData = _buildData.Price.Save(),
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            _structureGuid = saveData.StructureGuid;
            _structureData = saveData.StructureData;

            _buildData.Price.Load(saveData.BuildPriceData);
            
            SetItemsAmountData();
        }


        [Serializable]
        private struct SaveData
        {
            public string StructureGuid { get; set; }
            public object StructureData { get; set; }
            public object BuildPriceData { get; set; }
        }
    }
}