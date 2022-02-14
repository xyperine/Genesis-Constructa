using UnityEngine;

namespace MoonPioneerClone.Generator
{
    public class GeneratorLightingController : MonoBehaviour
    {
        [SerializeField] private Light coreLight;
        [SerializeField] private MeshRenderer meshRenderer;

        [SerializeField] private float lightLevel = 1f;

        private float defaultLightIntensity;
        private float defaultMaterialEmission;


#if UNITY_EDITOR
        private void Awake()
        {
            meshRenderer.material = new Material(meshRenderer.material);

            defaultLightIntensity = coreLight.intensity;
            defaultMaterialEmission = meshRenderer.material.GetFloat("_Emission_Strength");
        }
#endif


        private void Update()
        {
            coreLight.intensity = defaultLightIntensity * lightLevel;
            meshRenderer.material.SetFloat("_Emission_Strength", defaultMaterialEmission * lightLevel);
        }
    }
}
