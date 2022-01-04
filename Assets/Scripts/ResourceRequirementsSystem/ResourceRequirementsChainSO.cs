using System;
using UnityEngine;

namespace MoonPioneerClone.ResourceRequirementsSystem
{
    [CreateAssetMenu(fileName = "ResourceRequirementsChain", menuName = "Resource Requirements Chain")]
    public class ResourceRequirementsChainSO : ScriptableObject
    {
        [SerializeField] private ResourceRequirementsBlock[] blocks;

        private ResourceRequirementsBlock _activeRequirementBlock;

        public ResourceType[] RequiredResources => _activeRequirementBlock?.GetRequiredResources();
        public bool NeedMore => _activeRequirementBlock.NeedMore;
        public event Action BlockSatisfied;


        public bool NeedThisResource(ResourceType type)
        {
            return _activeRequirementBlock.NeedThisResource(type);
        }
        
        
        public void Add(ResourceType type)
        {
            _activeRequirementBlock.Add(type);
        }


        private void OnValidate()
        {
            if (blocks.Length < 1)
            {
                return;
            }
            
            SetupInitialBlock();

            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i].UpdateCounter();
                if (i == blocks.Length - 1)
                {
                    return;
                }

                int nextIndex = i + 1;
                blocks[i].SetNextBlock(blocks[nextIndex]);
            }
        }


        private void SetupInitialBlock()
        {
            _activeRequirementBlock = blocks[0];
            _activeRequirementBlock.Satisfied += SetupBlock;
            _activeRequirementBlock.Satisfied += InvokeBlockSatisfied;
        }
        
        
        private void SetupBlock()
        {
            _activeRequirementBlock.Satisfied -= SetupBlock;
            _activeRequirementBlock.Satisfied -= InvokeBlockSatisfied;
            
            _activeRequirementBlock = _activeRequirementBlock.NextBlock;

            if (_activeRequirementBlock == null)
            {
                return;
            }
            
            _activeRequirementBlock.Satisfied += SetupBlock;
            _activeRequirementBlock.Satisfied += InvokeBlockSatisfied;
        }


        private void InvokeBlockSatisfied()
        {
            BlockSatisfied?.Invoke();
        }
    }
}