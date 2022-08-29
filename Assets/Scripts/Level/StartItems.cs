using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.SaveLoadSystem;
using UnityEngine;

namespace ColonizationMobileGame.Level
{
    public class StartItems : MonoBehaviour, ISaveableWithGuid
    {
        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        private List<StackZoneItem> _items;

        public PermanentGuid Guid => guid;
        
        
        private void Awake()
        {
            _items = GetComponentsInChildren<StackZoneItem>(true).ToList();
        }


        public object Save()
        {
            return new SaveData
            {
                Items = _items.Select(i => i.Type).ToArray(),
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            foreach (ItemType itemType in saveData.Items)
            {
                StackZoneItem item = _items.FirstOrDefault(i => i.Type == itemType);
                
                if (!item)
                {
                    continue;
                }
                
                item.Return();
                _items.Remove(item);
            }
        }


        [Serializable]
        private struct SaveData
        {
            public ItemType[] Items { get; set; }
        }
    }
}