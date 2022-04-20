using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.StackZones
{
    public class StackZoneSetup : MonoBehaviour
    {
        [SerializeField, ShowIf(nameof(_setupMode))]
        [HideReferenceObjectPicker]
        [PropertySpace(SpaceAfter = 16f)]
        private StackZoneComponentsBuilder builder;

        [SerializeField, ShowIf(nameof(_setupMode))] 
        [HideReferenceObjectPicker] 
        [PropertySpace(SpaceAfter = 16f)]
        private StackZoneSetupData data;

        [SerializeField]
        [HideInInspector]
        private StackZoneSetupData savedData;
        
#pragma warning disable 0414
        private bool _setupMode;
#pragma warning restore 0414


        [PropertySpace(16f)]
        [Button(ButtonSizes.Large), HideIf(nameof(_setupMode))]
        public void Setup()
        {
            _setupMode = true;

            if (savedData == null)
            {
                return; 
            }
            
            data = new StackZoneSetupData(savedData);
        }
        
        
        [Button(ButtonSizes.Large, Name = "Reset"), ShowIf(nameof(_setupMode))]
        [ResponsiveButtonGroup("S", DefaultButtonSize = ButtonSizes.Large)]
        public void ResetZone()
        {
            data = new StackZoneSetupData();
        }

        
        [ShowIf(nameof(_setupMode))]
        [ResponsiveButtonGroup("S")]
        public void Accept()
        {
            _setupMode = false;

            savedData = data;
            
            PassData();
            
            EditorUtility.SetDirty(gameObject);
        }

        
        [Button(ButtonSizes.Small), ShowIf(nameof(_setupMode))]
        [ResponsiveButtonGroup("S")]
        public void Cancel()
        {
            _setupMode = false;

            data = savedData;
        }


        private void PassData()
        {
            builder.Build(gameObject, savedData);
        }
    }
}