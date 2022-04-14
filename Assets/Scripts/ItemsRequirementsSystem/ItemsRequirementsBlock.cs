using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.ItemsRequirementsSystem
{
    [Serializable]
    public sealed class ItemsRequirementsBlock
    {
        [TableList]
        [SerializeField] private ItemRequirement[] requirements;

        public ItemsRequirementsBlock NextBlock { get; private set; }
        public bool NeedMore => requirements.Any(r => !r.Satisfied);
        public ItemType[] RequiredItems => requirements.Where(r => !r.Satisfied).Select(r => r.Type).ToArray();
        
        public event Action Satisfied;


        public void SetNextBlock(ItemsRequirementsBlock block)
        {
            NextBlock = block;
        }
        

        public void AddItem(ItemType type)
        {
            PassToMatchingRequirement(type);
            
            CheckSatisfaction();
        }


        private void PassToMatchingRequirement(ItemType type)
        {
            ItemRequirement matchingRequirement = requirements.FirstOrDefault(r => r.Type == type);
            matchingRequirement?.AddOneItem();
        }


        private void CheckSatisfaction()
        {
            bool allRequirementsSatisfied = requirements.Aggregate(true, (satisfied, r) => satisfied & r.Satisfied);

            if (allRequirementsSatisfied)
            {
                Satisfied?.Invoke();
            }
        }
    }
}