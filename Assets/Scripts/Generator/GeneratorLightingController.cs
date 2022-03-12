using UnityEngine;

namespace MoonPioneerClone.Generator
{
    public sealed class GeneratorLightingController : MonoBehaviour
    {
        [SerializeField] private Light coreLight;
        [SerializeField] private MeshRenderer meshRenderer;

        [SerializeField] private float lightLevel = 1f;

        private float _defaultLightIntensity;
        private float _defaultMaterialEmission;


        private void Awake()
        {
#if UNITY_EDITOR
            meshRenderer.material = new Material(meshRenderer.material);
#endif
            _defaultLightIntensity = coreLight.intensity;
            _defaultMaterialEmission = meshRenderer.material.GetFloat("_Emission_Strength");
        }


        private void Update()
        {
            coreLight.intensity = _defaultLightIntensity * lightLevel;
            meshRenderer.material.SetFloat("_Emission_Strength", _defaultMaterialEmission * lightLevel);
        }
    }
}
