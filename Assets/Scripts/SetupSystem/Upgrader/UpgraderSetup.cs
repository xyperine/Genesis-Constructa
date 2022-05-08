using UnityEditor;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.Upgrader
{
    public abstract class UpgraderSetup<TUpgraderSetupData, TBuilder> : MonoBehaviour
        where TUpgraderSetupData : UpgraderSetupData
        where TBuilder : UpgraderComponentsBuilder<TUpgraderSetupData>
    {
        [SerializeField] protected TBuilder builder;


        public void SetData(TUpgraderSetupData setupData)
        {
            builder.SetData(setupData);
            builder.SetupCollider();
            
#if UNITY_EDITOR
            EditorUtility.SetDirty(gameObject);
#endif
        }


        private void Start()
        {
            Setup();
        }


        private void Setup()
        {
            builder.Build();
        }
    }
}