using System;
using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.SaveLoadSystem;
using UnityEngine;

namespace ColonizationMobileGame.Structures
{
    public class StructureRuntimeSaveResolver : MonoBehaviour, ISaveableWithGuid
    {
        [SerializeField] private bool active;
        
        [SerializeField, HideInInspector] private PermanentGuid guid;

        private StackZone[] _allStackZones;

        public SaveableType SaveableType => SaveableType.RuntimeBuiltStructure;
        public PermanentGuid Guid => guid;


        private void Awake()
        {
            if (!active)
            {
                Destroy(this);
            }
            
            _allStackZones = GetComponentsInChildren<StackZone>();
        }


        public object Save()
        {
            foreach (StackZone zone in _allStackZones)
            {
                zone.Guid.Set(string.IsNullOrEmpty(zone.Guid.Value) ? PermanentGuid.NewGuid() : zone.Guid.Value);
            }
            
            return new SaveData
            {
                StackZonesGuids = _allStackZones?.Select(z => z.Guid.Value).ToArray(),
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            for (int i = 0; i < _allStackZones.Length; i++)
            {
                _allStackZones[i].Guid.Set(saveData.StackZonesGuids[i]);
            }
        }

        
        [Serializable]
        private struct SaveData
        {
            public string[] StackZonesGuids { get; set; }
        }
    }
}