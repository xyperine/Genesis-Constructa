using Sirenix.OdinInspector;
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
        private StackZoneSetupData data = new StackZoneSetupData();

        private StackZoneSetupData _savedData;
        
#pragma warning disable 0414
        private bool _setupMode;
#pragma warning restore 0414


        [PropertySpace(16f)]
        [Button(ButtonSizes.Large), HideIf(nameof(_setupMode))]
        public void Setup()
        {
            _setupMode = true;

            if (_savedData == null)
            {
                return;
            }
            
            data = new StackZoneSetupData(_savedData);
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

            _savedData = data;
            
            PassData();
        }

        
        [Button(ButtonSizes.Small), ShowIf(nameof(_setupMode))]
        [ResponsiveButtonGroup("S")]
        public void Cancel()
        {
            _setupMode = false;

            data = _savedData;
        }


        private void PassData()
        {
            builder.Build(gameObject, _savedData);
        }
    }
}