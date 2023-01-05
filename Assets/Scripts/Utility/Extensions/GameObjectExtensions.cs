using System;
using System.Linq;
using ColonizationMobileGame.SetupSystem;
using Shapes;
using UnityEngine;

namespace ColonizationMobileGame.Utility.Extensions
{
    public static class GameObjectExtensions
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