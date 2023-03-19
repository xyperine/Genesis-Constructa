using System;
using UnityEngine;

namespace ColonizationMobileGame.Utility
{
    [Serializable]
    public class Range : ISerializationCallbackReceiver
    {
        [field: SerializeField] public float Min { get; set; }
        [field: SerializeField] public float Max { get; set; }
        
        
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