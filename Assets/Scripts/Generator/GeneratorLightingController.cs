using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.Generator
{
    public sealed class GeneratorLightingController : MonoBehaviour
    {
        [SerializeField] private Light coreLight;
        [SerializeField] private MeshRenderer meshRenderer;

        [SerializeField] private float lightLevel = 1f;

        private static readonly int EmissionStrengthPropertyID = Shader.PropertyToID("_Emission_Strength");
        
        private float _defaultLightIntensity;
        private float _defaultMaterialEmission;


        private void Awake()
        {
#if UNITY_EDITOR
            meshRenderer.material = new Material(meshRenderer.material);
#endif
            _defaultLightIntensity = coreLight.intensity;
            _defaultMaterialEmission = meshRenderer.material.GetFloat(EmissionStrengthPropertyID);
        }


        private void Update()
        {
            coreLight.intensity = _defaultLightIntensity * lightLevel;
            meshRenderer.material.SetFloat(EmissionStrengthPropertyID, _defaultMaterialEmission * lightLevel);
        }
    }
}
