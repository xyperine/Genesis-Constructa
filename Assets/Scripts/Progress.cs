using UnityEngine;

namespace ColonizationMobileGame
{
    public class Progress
    {
        public int Current { get; }
        public int Required { get; }
        public float Percentage => (float) Current / Required * 100f;

        public bool Complete => Current >= Required;


        public Progress(int current, int required)
        {
            Current = Mathf.Min(current, required);
            Required = required;
        }
    }
}