using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction
{
    [CreateAssetMenu(fileName = "Extractor_Production_Rate", menuName = "Extractor/Production Rate", order = 0)]
    public sealed class ExtractorProductionRateSO : ScriptableObject
    {
        [SerializeField, Range(0.5f, 120f)] private float secondsPerItem;
        [SerializeField, ReadOnly] private float itemsPerSecond;
        
        [ShowInInspector, ReadOnly] private float _itemsPerMinute;
        
        public float SecondsPerItem => secondsPerItem;
        public float ItemsPerSecond => itemsPerSecond;


        private void OnValidate()
        {
            itemsPerSecond = 1f / secondsPerItem;
            _itemsPerMinute = itemsPerSecond * 60f;
        }
    }
}