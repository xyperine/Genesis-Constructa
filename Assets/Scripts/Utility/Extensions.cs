using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.SetupSystem;
using UnityEngine;

namespace ColonizationMobileGame.Utility
{
    public static class Extensions
    {
        public static GameObject GetChildByMarker(this GameObject rootObj, Type markerType)
        {
            if (!markerType.IsSubclassOf(typeof(SetupMarker)))
            {
                throw new ArgumentException($"Passed type is not deriving from {typeof(SetupMarker)}!");
            }
            
            GameObject obj = rootObj.GetComponentsInChildren<SetupMarker>()
                .SingleOrDefault(m => m.GetType() == markerType)?.gameObject;

            return obj;
        }


        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
        

        public static Dictionary<string, ItemType[]> MapItemsToZones(this IEnumerable<StackZoneItem> itemsCollection)
        {
            StackZoneItem[] itemsInZones = itemsCollection.Where(i => i.Zone).ToArray();
            string[] zonesGuids = itemsInZones.Select(i => i.Zone.Guid.Value).Distinct().ToArray();

            return zonesGuids.ToDictionary(g => g,
                g => itemsInZones.Where(i => i.Zone.Guid.Value == g).Select(i => i.Type).ToArray());
        }


        public static Vector2 XZPlaneVector2(this Vector3 a)
        {
            return new Vector2(a.x, a.z);
        }
        
        
        public static Vector3 XZPlane(this Vector3 a)
        {
            return new Vector3(a.x, 0f, a.z);
        }


        public static Vector3 XZPlaneToVector3(this Vector2 a, float y = 0f)
        {
            return new Vector3(a.x, y, a.y);
        }
    }
}