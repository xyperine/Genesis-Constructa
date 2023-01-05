using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.SetupSystem;
using Shapes;
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


        public static Bounds GetGameObjectBounds(this GameObject gameObject)
        {
            Transform referenceTransform = gameObject.transform;
            Bounds b = new Bounds(Vector3.zero, Vector3.zero);
            RecurseEncapsulate(referenceTransform, ref b);
            return b;
                       
            void RecurseEncapsulate(Transform child, ref Bounds bounds)
            {
                if (child.GetComponent<Canvas>())
                {
                    return;
                }

                if (child.GetComponent<Rectangle>())
                {
                    return;
                }
                
                MeshFilter mesh = child.GetComponent<MeshFilter>();
                if (mesh)
                {
                    Bounds lsBounds = mesh.sharedMesh.bounds;
                    Vector3 wsMin = child.TransformPoint(lsBounds.center - lsBounds.extents);
                    Vector3 wsMax = child.TransformPoint(lsBounds.center + lsBounds.extents);
                    bounds.Encapsulate(referenceTransform.InverseTransformPoint(wsMin));
                    bounds.Encapsulate(referenceTransform.InverseTransformPoint(wsMax));
                }
                foreach (Transform grandChild in child.transform)
                {
                    RecurseEncapsulate(grandChild, ref bounds);
                }
            }
        }
    }
}