using System;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.StackZone
{
    [Serializable]
    public class StackZoneBoxColliderSetupData : StackZoneColliderSetupData
    {
        [SerializeField] private Vector3 size;

        public override Type ColliderType { get; } = typeof(BoxCollider);
    }
}