using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction.Upgrading
{
    [CreateAssetMenu(fileName = "Extractor_Upgrades", menuName = "Upgrades/Extractor", order = 0)]
    public sealed class ExtractorUpgradesSO : ScriptableObject
    {
        [SerializeField] private ExtractorUpgradeData[] upgrades = { };
        
        private int _index;

        public int Capacity => upgrades[_index].Capacity;
        public float ItemsPerSecond => upgrades[_index].ItemsPerSecond;


        private void OnEnable()
        {
            _index = 0;
        }


        public void Upgrade()
        {
            if (_index >= upgrades.Length)
            {
                return;
            }
            
            _index++;
        }
    }
}