using System;
using ColonizationMobileGame.ItemsExtraction.Upgrading;
using ColonizationMobileGame.SaveLoadSystem;
using UnityEngine;

namespace ColonizationMobileGame.Structures
{
    public class Structure : MonoBehaviour, ISaveableWithGuid
    {
        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        private ExtractorUpgrader _upgrader;

        public int Level => _upgrader? _upgrader.Level : 0;
        public StructureType Type { get; private set; }
        public int MaxLevel { get; private set; }

        public PermanentGuid Guid => guid;


        public void Setup(StructureType structureType, int maxLevel)
        {
            Type = structureType;
            MaxLevel = maxLevel;

            _upgrader = GetComponentInChildren<ExtractorUpgrader>();
        }


        public object Save()
        {
            return new SaveData
            {
                
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;
        }

        
        [Serializable]
        private struct SaveData
        {
            public string[] StackZonesGuids { get; set; }
        }
    }
}