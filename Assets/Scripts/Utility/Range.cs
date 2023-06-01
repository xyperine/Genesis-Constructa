using System;
using UnityEngine;

namespace GenesisConstructa.Utility
{
    [Serializable]
    public class Range : ISerializationCallbackReceiver
    {
        [field: SerializeField] public float Min { get; set; }
        [field: SerializeField] public float Max { get; set; }
        
        
        public float Random()
        {
            return UnityEngine.Random.Range(Min, Max);
        }
        
        
        public void OnBeforeSerialize()
        {
            if (Min > Max)
            {
                throw new ArithmeticException($"{nameof(Min)} = {Min} cannot be bigger than {nameof(Max)} = {Max}");
            }
        }


        public void OnAfterDeserialize() { }
    }
}