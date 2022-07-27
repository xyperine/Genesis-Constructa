﻿using System;
using System.Linq;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using ColonizationMobileGame.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.ItemsRequirementsSystem
{
    [Serializable]
    public sealed class ItemsRequirementsBlock : IDeepCloneable<ItemsRequirementsBlock>
    {
        [TableList]
        [SerializeField] private ItemRequirement[] requirements;

        public bool Locked { get; set; }
        public ItemsRequirementsBlock NextBlock { get; private set; }
        public bool NeedMore => requirements.Any(r => !r.Fulfilled);
        public ItemType[] RequiredItems => requirements.Where(r => !r.Fulfilled).Select(r => r.Type).ToArray();

        public event Action Fulfilled;


        public ItemAmountData[] ToItemsAmount()
        {
            return requirements.Select(r => new ItemAmountData(r.Type, r.CurrentAmount, r.Required)).ToArray();
        }
        
        
        public void SetNextBlock(ItemsRequirementsBlock block)
        {
            NextBlock = block;
        }
        

        public void AddItem(ItemType type)
        {
            PassToMatchingRequirement(type);
            
            CheckFulfilment();
        }


        private void PassToMatchingRequirement(ItemType type)
        {
            ItemRequirement matchingRequirement = requirements.FirstOrDefault(r => r.Type == type);
            matchingRequirement?.AddOneItem();
        }


        private void CheckFulfilment()
        {
            bool allRequirementsFulfilled = requirements.All(r => r.Fulfilled);

            if (allRequirementsFulfilled)
            {
                Fulfilled?.Invoke();
            }
        }


        public ItemsRequirementsBlock GetDeepCopy()
        {
            ItemsRequirementsBlock block = new ItemsRequirementsBlock
            {
                Locked = Locked,
                requirements = requirements.Select(r => (ItemRequirement) r.Clone()).ToArray(),
            };

            return block;
        }
    }
}