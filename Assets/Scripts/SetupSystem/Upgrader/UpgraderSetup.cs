using UnityEditor;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.Upgrader
{
    public abstract class UpgraderSetup<TSetupData, TBuilder> : MonoBehaviour
        where TSetupData : UpgraderSetupData
        where TBuilder : UpgraderComponentsBuilder<TSetupData>
    {
        [SerializeField] protected TBuilder builder;


        public void SetData(TSetupData setupData)
        {
            builder.SetData(setupData);
            builder.SetupCollider();
            
            EditorUtility.SetDirty(gameObject);
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