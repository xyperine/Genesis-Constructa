using System;
using UnityEngine;

namespace ColonizationMobileGame.ScoreSystem
{
    public class Score : MonoBehaviour
    {
        public int Value { get; private set; }
        
        
        public void Add(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            
            Value += value;
            
            Debug.Log(Value);
        }
    }
}