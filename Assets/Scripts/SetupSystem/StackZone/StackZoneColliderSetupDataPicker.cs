using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.StackZone
{
    [Serializable]
    public class StackZoneColliderSetupDataPicker
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        [TabGroup("Sphere")]
        private StackZoneSphereColliderSetupData sphereColliderSetupData = new StackZoneSphereColliderSetupData();
        
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        [TabGroup("Box")]
        private StackZoneBoxColliderSetupData boxColliderSetupData = new StackZoneBoxColliderSetupData();
        
        public StackZoneColliderSetupData SelectedColliderSetupData { get; private set; }
    }
}