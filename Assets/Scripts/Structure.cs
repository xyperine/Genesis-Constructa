using ColonizationMobileGame.ItemsExtraction.Upgrading;
using UnityEngine;

namespace ColonizationMobileGame
{
    public class Structure : MonoBehaviour
    {
        private ExtractorUpgrader _upgrader;

        public int Level => _upgrader? _upgrader.Level : 0;
        public StructureType Type { get; private set; }
        public int MaxLevel { get; private set; }


        public void Setup(StructureType structureType, int maxLevel)
        {
            Type = structureType;
            MaxLevel = maxLevel;

            _upgrader = GetComponentInChildren<ExtractorUpgrader>();
        }
    }
}