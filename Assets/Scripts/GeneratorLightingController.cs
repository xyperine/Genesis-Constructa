using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoonPioneerClone
{
    public class GeneratorLightingController : MonoBehaviour
    {
        [SerializeField] private Light coreLight;
        [SerializeField] private MeshRenderer renderer;

        [SerializeField] private float lightLevel = 1f;

        private float defaultLightIntensity;
        private float defaultMaterialEmission;


#if UNITY_EDITOR
        private void Awake()
        {
            renderer.material = new Material(renderer.material);

            defaultLightIntensity = coreLight.intensity;
            defaultMaterialEmission = renderer.material.GetFloat("_Emission_Strength");
        }
#endif


        private void Update()
        {
            coreLight.intensity = defaultLightIntensity * lightLevel;
            renderer.material.SetFloat("_Emission_Strength", defaultMaterialEmission * lightLevel);
        }
    }
}
