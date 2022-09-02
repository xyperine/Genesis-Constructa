using System;
using System.Linq;
using ColonizationMobileGame.ItemsExtraction.Upgrading;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.SaveLoadSystem;
using UnityEngine;

namespace ColonizationMobileGame.Structures
{
    public class StructureRuntimeSaveResolver : MonoBehaviour, ISaveableWithGuid
    {
        [SerializeField] private bool active;
        
        [SerializeField, HideInInspector] private PermanentGuid guid;

        private ExtractorUpgrader _upgrader;
        private StackZone[] _allStackZones;

        public SaveableType SaveableType => SaveableType.RuntimeBuiltStructure;
        public PermanentGuid Guid => guid;


        private void Awake()
        {
            if (!active)
            {
                Destroy(this);
            }

            _upgrader = GetComponentInChildren<ExtractorUpgrader>();
            _allStackZones = GetComponentsInChildren<StackZone>();
        }


        public object Save()
        {
            SetGuids();
            
            return new SaveData
            {
                UpgraderGuid = _upgrader.Guid.Value,
                UpgraderData = _upgrader.Save(),
                StackZonesGuids = _allStackZones?.Select(z => z.Guid.Value).ToArray(),
            };
        }


        private void SetGuids()
        {
            _upgrader.Guid.Set(PermanentGuid.NewGuid());
            foreach (StackZone zone in _allStackZones)
            {
                zone.Guid.Set(string.IsNullOrEmpty(zone.Guid.Value) ? PermanentGuid.NewGuid() : zone.Guid.Value);
            }
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            _upgrader.Guid.Set(saveData.UpgraderGuid);
            _upgrader.Load(saveData.UpgraderData);
            
            for (int i = 0; i < _allStackZones.Length; i++)
            {
                _allStackZones[i].Guid.Set(saveData.StackZonesGuids[i]);
            }
        }

        
        [Serializable]
        private struct SaveData
        {
            public string UpgraderGuid { get; set; }
            public object UpgraderData { get; set; }
            public string[] StackZonesGuids { get; set; }
        }
    }
}