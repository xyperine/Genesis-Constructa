using MoonPioneerClone.ItemsExtraction.Upgrading;
using MoonPioneerClone.UpgradingSystem;
using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction
{
    [CreateAssetMenu(fileName = "Extractor_Production_Rate", menuName = "Extractor/Production Rate", order = 0)]
    public sealed class ExtractorProductionRateSO : ScriptableObject, IUpgradeable<ExtractorUpgradeData>
    {
        [SerializeField] private float itemsPerSecond;


        public float ItemsPerSecond { get; private set; }


#if !UNITY_EDITOR
        private void OnEnable()
        {
            ItemsPerSecond = itemsPerSecond;
        }
#endif


        private void OnValidate()
        {
            ItemsPerSecond = itemsPerSecond;
        }


        public void Upgrade(ExtractorUpgradeData data)
        {
            ItemsPerSecond = data.ItemsPerSecond;
        }
    }
}