using System;
using System.Linq;
using UnityEngine;

namespace MoonPioneerClone.ResourceRequirementsSystem
{
    [CreateAssetMenu(fileName = "ResourceRequirementsChain", menuName = "Resource Requirements Chain")]
    public sealed class ResourceRequirementsChainSO : ScriptableObject
    {
        [SerializeField] private ResourceRequirementsBlock[] blocks;

        private ResourceRequirementsBlock _activeRequirementBlock;

        public ResourceType[] RequiredResources => _activeRequirementBlock?.RequiredResources;
        public bool NeedMore => _activeRequirementBlock.NeedMore;
        public event Action BlockSatisfied;


        public bool NeedResource(ResourceType type)
        {
            return RequiredResources.Contains(type);
        }
        
        
        public void AddResource(ResourceType type)
        {
            _activeRequirementBlock.AddResource(type);
        }


        private void OnValidate()
        {
            SetupChain();
        }


        private void SetupChain()
        {
            if (blocks.Length < 1)
            {
                return;
            }
            
            SetupInitialBlock();

            for (int i = 0; i < blocks.Length; i++)
            {
                SetupBlock(i);
            }
        }


        private void SetupInitialBlock()
        {
            _activeRequirementBlock = blocks[0];
            
            SubscribeToActiveBlockSatisfaction();
        }


        private void SetupBlock(int index)
        {
            ResourceRequirementsBlock block = blocks[index];
            block.UpdateCounter();
            
            if (index == blocks.Length - 1)
            {
                return;
            }
            
            block.SetNextBlock(blocks[index + 1]);
        }


        private void SubscribeToActiveBlockSatisfaction()
        {
            if (_activeRequirementBlock == null)
            {
                return;
            }
            
            _activeRequirementBlock.Satisfied += GoToNextBlock;
            _activeRequirementBlock.Satisfied += InvokeBlockSatisfied;
        }


        private void UnsubscribeFromActiveBlockSatisfaction()
        {
            if (_activeRequirementBlock == null)
            {
                return;
            }
            
            _activeRequirementBlock.Satisfied -= GoToNextBlock;
            _activeRequirementBlock.Satisfied -= InvokeBlockSatisfied;
        }
        
        
        private void GoToNextBlock()
        {
            UnsubscribeFromActiveBlockSatisfaction();
            
            _activeRequirementBlock = _activeRequirementBlock.NextBlock;

            SubscribeToActiveBlockSatisfaction();
        }


        private void InvokeBlockSatisfied()
        {
            BlockSatisfied?.Invoke();
        }
    }
}