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
        public event Action Satisfied;
        public bool NeedMore => requirements.Any(r => !r.Satisfied);


        public bool NeedThisResource(ResourceType type)
        {
            return requirements.Any(r => !r.Satisfied && r.Type == type);
        }


        public ResourceType[] GetRequiredResources()
        {
            return requirements.Where(r => !r.Satisfied).Select(r => r.Type).ToArray();
        }


        public void SetNextBlock(ResourceRequirementsBlock block)
        {
            NextBlock = block;
        }
        

        public void Add(ResourceType type)
        {
            ResourceRequirement matchingRequirement = requirements.FirstOrDefault(r => r.Type == type);
            if (matchingRequirement == null)
            {
                return;
            }
            matchingRequirement.Add();
            
            CheckSatisfaction();
        }


        private void CheckSatisfaction()
        {
            bool allRequirementsSatisfied = requirements.Aggregate(true, (s, r) => s & r.Satisfied);
            Debug.Log(allRequirementsSatisfied);
            if (allRequirementsSatisfied)
            {
                Satisfied?.Invoke();
                Debug.Log("SATISFIED");
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