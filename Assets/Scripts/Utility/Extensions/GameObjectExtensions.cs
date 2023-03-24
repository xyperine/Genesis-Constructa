using System;
using System.Linq;
using System.Text;
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
        

        public static Bounds GetBounds(this GameObject gameObject)
        {
            MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();

            Vector3 min = Vector3.positiveInfinity;
            Vector3 max = Vector3.negativeInfinity;
            
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                if (meshRenderer.GetComponent<ShapeRenderer>())
                {
                    continue;
                }
                
                Bounds meshRendererBounds = meshRenderer.bounds;

                for (int n = 0; n < 3; n++)
                {
                    min[n] = Mathf.Min(min[n], meshRendererBounds.min[n]);
                    max[n] = Mathf.Max(max[n], meshRendererBounds.max[n]);
                }
            }

            if (min == Vector3.positiveInfinity || max == Vector3.negativeInfinity)
            {
                min = Vector3.zero;
                max = Vector3.zero;
                
                Debug.LogWarning("No mesh renderers found or their bounds are too big!");
            }
            
            Bounds bounds = new Bounds();
            bounds.SetMinMax(min, max);

            return bounds;
        }


        // For debugging purposes
        public static string GetFullName (this GameObject gameObject)
        {
            StringBuilder nameBuilder = new StringBuilder(gameObject.name);
            
            while (gameObject.transform.parent != null) 
            {
                gameObject = gameObject.transform.parent.gameObject;
                nameBuilder.Insert(0, gameObject.name + "/");
            }
            
            return nameBuilder.ToString();
        }
    }
}