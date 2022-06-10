using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace ColonizationMobileGame.Player
{
    public class RobotAntennasBlinkingBehaviour : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MeshRenderer headRenderer;
        [SerializeField] private Material smallAntennaTipMaterial;
        [SerializeField] private Material bigAntennaTipMaterial;
        [Header("Blinking")]
        [SerializeField, Range(5f, 15f)] private float maxIntensity;
        [SerializeField] private float blinkingInterval;
        [SerializeField] private float blinkingDuration;
        [SerializeField, Range(0f, 1f)] private float syncFactor;
        [SerializeField] private AnimationCurve blinkingCurve;

        private const string EMISSION_KEYWORD_NAME = "_EMISSION";
        private readonly int _emissionColorPropertyID = Shader.PropertyToID("_EmissionColor");

        private Material _smallMaterialCopy;
        private Material _bigMaterialCopy;
        

        private void Start()
        {
            SetupMaterials();
            SetupSequence();
        }


        private void SetupMaterials()
        {
            List<Material> materials = new List<Material>(headRenderer.materials);
            
            int smallIndex = materials.FindIndex(m => m.name.Contains(smallAntennaTipMaterial.name));
            _smallMaterialCopy = new Material(smallAntennaTipMaterial);
            _smallMaterialCopy.EnableKeyword(EMISSION_KEYWORD_NAME);
            materials[smallIndex] = _smallMaterialCopy;
            
            int bigIndex = materials.FindIndex(m => m.name.Contains(bigAntennaTipMaterial.name));
            _bigMaterialCopy = new Material(bigAntennaTipMaterial);
            _bigMaterialCopy.EnableKeyword(EMISSION_KEYWORD_NAME);
            materials[bigIndex] = _bigMaterialCopy;
            
            headRenderer.materials = materials.ToArray();
        }


        private void SetupSequence()
        {
            Color smallAntennaColorTip = Color.white * Mathf.Pow(2f, maxIntensity);
            Color bigAntennaColorTip = Color.white * Mathf.Pow(2f, maxIntensity);
            
            Sequence sequence = DOTween.Sequence().Append(
                _smallMaterialCopy.DOColor(smallAntennaColorTip, _emissionColorPropertyID, blinkingDuration).SetEase(blinkingCurve)
            ).Insert(blinkingDuration * syncFactor,
                _bigMaterialCopy.DOColor(bigAntennaColorTip, _emissionColorPropertyID, blinkingDuration).SetEase(blinkingCurve)
            ).SetLoops(-1).SetDelay(blinkingInterval);
        }
    }
}