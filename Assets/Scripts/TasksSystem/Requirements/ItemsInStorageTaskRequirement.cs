using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsRequirementsSystem;
using UnityEngine;

namespace ColonizationMobileGame.TasksSystem.Requirements
{
    [Serializable]
    public class ItemsInStorageTaskRequirement : TaskRequirement
    {
        [SerializeField] private ItemRequirement[] itemsRequired;
        
        public override Progress Progress => GetProgress();
        
        
        protected override void Subscribe()
        {
            data.LevelData.Changed += OnDataChanged;
        }


        protected override void Unsubscribe()
        {
            data.LevelData.Changed -= OnDataChanged;
        }


        private Progress GetProgress()
        {
            IEnumerable<KeyValuePair<ItemType, uint>> filteredItemsInStorage = data.LevelData.ItemsInStorage
                .Where(kvp => itemsRequired
                    .Select(r => r.Type)
                    .Contains(kvp.Key));
            int current = filteredItemsInStorage
                .Where(kvp => (int) kvp.Value >= itemsRequired.SingleOrDefault(r => r.Type == kvp.Key)?.Required)
                .Sum(kvp => (int) kvp.Value);
            int required = itemsRequired.Sum(r => r.Required);

            return new Progress(current, required);
        }
    }
}