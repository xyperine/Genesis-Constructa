using System;
using System.Linq;
using ColonizationMobileGame.SetupSystem;
using UnityEngine;

namespace ColonizationMobileGame.Utility
{
    public static class Extensions
    {
        public static GameObject GetGameObjectByMarker(this GameObject rootObj, Type markerType)
        {
            if (!markerType.IsSubclassOf(typeof(SetupMarker)))
            {
                throw new ArgumentException($"Passed type is not deriving from {typeof(SetupMarker)}!");
            }
            
            GameObject obj = rootObj.GetComponentsInChildren<SetupMarker>()
                .SingleOrDefault(m => m.GetType() == markerType)?.gameObject;

            return obj;
        }
    }
}