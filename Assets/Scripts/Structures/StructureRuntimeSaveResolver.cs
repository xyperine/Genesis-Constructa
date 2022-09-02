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


        public PermanentGuid Guid => guid;


        private void Awake()
        {
            if (!active)
            {
                Destroy(this);
            }
        }


        public object Save()
        {
            return new SaveData
            {
            };
        }


        public void Load(object data)
        {
        }

        
        [Serializable]
        private struct SaveData
        {
        }
    }
}