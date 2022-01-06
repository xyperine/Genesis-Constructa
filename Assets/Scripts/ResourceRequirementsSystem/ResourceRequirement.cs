using System;
using UnityEngine;

namespace MoonPioneerClone.ResourceRequirementsSystem
{
    [Serializable]
    public sealed class ResourceRequirement
    {
        [SerializeField] private ResourceType type;
        [SerializeField, Min(1)] private int amount;
        
        private int _leftToSatisfaction;

        public ResourceType Type => type;
        public bool Satisfied => _leftToSatisfaction <= 0;
        

        public void AddOneResource()
        {
            _leftToSatisfaction--;
        }


#if UNITY_EDITOR
        public void UpdateCounter()
        {
            _leftToSatisfaction = amount;
        }
#endif
    }
}