using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.SetupSystem.StackZones
{
    [Serializable]
    public class StackZoneColliderPicker
    {
        private enum StackZoneColliderShape
        {
            Box,
            Sphere,
        }

        [SerializeField]
        [EnumToggleButtons]
        private StackZoneColliderShape shape;
        
        public Type SelectedColliderType
        {
            get
            {
                return shape switch
                {
                    StackZoneColliderShape.Box => typeof(BoxCollider),
                    StackZoneColliderShape.Sphere => typeof(SphereCollider),
                    _ => throw new ArgumentOutOfRangeException(),
                };
            }
        }
    }
}