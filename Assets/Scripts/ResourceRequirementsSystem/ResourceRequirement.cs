using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.ResourceRequirementsSystem
{
    [Serializable]
    public class ResourceRequirement
    {
        [SerializeField] private ResourceType type;
        [SerializeField, Min(1), OnValueChanged("@_leftToSatisfaction = amount")] private int amount;
        
        private int _leftToSatisfaction;

        public ResourceType Type => type;
        public bool Satisfied => _leftToSatisfaction <= 0;
        

        public void Add()
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