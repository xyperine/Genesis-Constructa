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


        private void Start()
        {
            _previousFrameState = Met;
        }


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
            print($"{gameObject}, met {met}, prev {_previousFrameState}");
            _previousFrameState = met;
            Changed?.Invoke();
        }
    }
}