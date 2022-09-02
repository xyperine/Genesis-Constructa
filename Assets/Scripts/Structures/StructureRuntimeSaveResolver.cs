using System;
using System.Linq;
using ColonizationMobileGame.ItemsExtraction.Extra.KeyItemSystem;
using ColonizationMobileGame.ItemsExtraction.Upgrading;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.SaveLoadSystem;
using UnityEngine;

namespace ColonizationMobileGame.Structures
{
    public class StructureRuntimeSaveResolver : MonoBehaviour, ISaveable, IPermanentGuidIdentifiable
    {
        private ExtractorUpgrader _upgrader;
        private StackZone[] _allStackZones;

        public PermanentGuid Guid { get; } = new PermanentGuid();


        private void Awake()
        {
            _upgrader = GetComponentInChildren<ExtractorUpgrader>();
            _allStackZones = GetComponentsInChildren<StackZone>();
        }


        private void Start()
        {
            SetGuids();
        }


        private void SetGuids()
        {
            _upgrader.Guid.Set(PermanentGuid.NewGuid());
            foreach (StackZone zone in _allStackZones)
            {
                zone.Guid.Set(string.IsNullOrEmpty(zone.Guid.Value) ? PermanentGuid.NewGuid() : zone.Guid.Value);
            }
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