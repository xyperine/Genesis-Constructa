using System;
using System.Linq;
using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction.Conditions
{
    [Serializable]
    public class ExtractorConditionsSet : MonoBehaviour
    {
        [SerializeField] private ExtractorCondition[] conditions;

        private bool _previousFrameState;

        public event Action Changed;

        public bool Met => conditions.Length == 0 || conditions.All(c => c.Met());


        private void Update()
        {
            DetectChange();
        }


        private void DetectChange()
        {
            bool met = Met;
            if (met == _previousFrameState)
            {
                return;
            }

            Changed?.Invoke();
            _previousFrameState = met;
        }
    }
}