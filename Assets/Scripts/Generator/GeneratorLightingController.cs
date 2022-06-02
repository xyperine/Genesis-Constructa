using UnityEngine;

namespace MoonPioneerClone.Generator
{
    public sealed class GeneratorLightingController : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private float lightLevel = 1f;

        private static readonly int EmissionStrengthPropertyID = Shader.PropertyToID("_Emission_Strength");

        private Material _material;
        
        private float _defaultEmission;


        private void Awake()
        {
            _material = meshRenderer.material;
            
            _defaultEmission = _material.GetFloat(EmissionStrengthPropertyID);
        }


        private void Update()
        {
            _material.SetFloat(EmissionStrengthPropertyID, _defaultEmission * lightLevel);
        }
    }
}
