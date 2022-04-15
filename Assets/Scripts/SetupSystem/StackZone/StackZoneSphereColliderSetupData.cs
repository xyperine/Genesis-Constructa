using System;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.StackZone
{
    [Serializable]
    public class StackZoneSphereColliderSetupData : StackZoneColliderSetupData
    {
        [SerializeField] private float radius;

        public override Type ColliderType { get; } = typeof(SphereCollider);
    }
}