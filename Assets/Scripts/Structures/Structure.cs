using DG.Tweening;
using GenesisConstructa.ItemsExtraction.Upgrading;
using UnityEngine;

namespace GenesisConstructa.Structures
{
    public class Structure : MonoBehaviour
    {
        [SerializeField] private Transform modelTransform;

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


        public void Appear(float duration, AnimationCurve easingCurve)
        {
            modelTransform.localScale = Vector3.up;
            modelTransform.DOScale(Vector3.one, duration).SetEase(easingCurve);
        }
    }
}