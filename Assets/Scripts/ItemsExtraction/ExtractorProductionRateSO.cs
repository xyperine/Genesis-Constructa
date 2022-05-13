using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction
{
    [CreateAssetMenu(fileName = "Extractor_Production_Rate", menuName = "Extractor/Production Rate", order = 0)]
    public sealed class ExtractorProductionRateSO : ScriptableObject
    {
        [SerializeField] private float itemsPerSecond;

        public float ItemsPerSecond => itemsPerSecond;
    }
}