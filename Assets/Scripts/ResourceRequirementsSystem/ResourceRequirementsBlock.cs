using System;
using System.Linq;
using UnityEngine;

namespace MoonPioneerClone.ResourceRequirementsSystem
{
    [Serializable]
    public sealed class ResourceRequirementsBlock
    {
        [SerializeField] private ResourceRequirement[] requirements;

        public ResourceRequirementsBlock NextBlock { get; private set; }
        public bool NeedMore => requirements.Any(r => !r.Satisfied);
        public ResourceType[] RequiredResources => requirements.Where(r => !r.Satisfied).Select(r => r.Type).ToArray();
        
        public event Action Satisfied;


        public void SetNextBlock(ResourceRequirementsBlock block)
        {
            NextBlock = block;
        }
        

        public void AddResource(ResourceType type)
        {
            PassToMatchingRequirement(type);
            
            CheckSatisfaction();
        }


        private void PassToMatchingRequirement(ResourceType type)
        {
            ResourceRequirement matchingRequirement = requirements.FirstOrDefault(r => r.Type == type);
            matchingRequirement?.AddOneResource();
        }


        private void CheckSatisfaction()
        {
            bool allRequirementsSatisfied = requirements.Aggregate(true, (s, r) => s & r.Satisfied);

            if (allRequirementsSatisfied)
            {
                Satisfied?.Invoke();
            }
        }
        
        
#if UNITY_EDITOR
        public void UpdateCounter()
        {
            foreach (ResourceRequirement requirement in requirements)
            {
                requirement.UpdateCounter();
            }
        }
#endif
    }
}