using System;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.StackZone
{
    public abstract class StackZoneColliderSetupData
    {
        [SerializeField] private Vector3 center;
        
        public abstract Type ColliderType { get; }
    }
}