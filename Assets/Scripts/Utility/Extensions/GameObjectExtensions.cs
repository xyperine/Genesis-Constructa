﻿using System;
using System.Linq;
using System.Text;
using ColonizationMobileGame.SetupSystem;
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
            MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();

            Vector3 min = Vector3.positiveInfinity;
            Vector3 max = Vector3.negativeInfinity;
            
            foreach (MeshFilter meshFilter in meshFilters)
            {
                if (!meshFilter.sharedMesh)
                {
                    continue;
                }
                
                Vector3[] vertices = meshFilter.sharedMesh.vertices;

                foreach (Vector3 vertex in vertices)
                {
                    Vector3 wsVertexPosition = meshFilter.transform.TransformPoint(vertex);
                    
                    for (int n = 0; n < 3; n++)
                    {
                        min[n] = Mathf.Min(min[n], wsVertexPosition[n]);
                        max[n] = Mathf.Max(max[n], wsVertexPosition[n]);
                    }
                }
            }
            
            Bounds bounds = new Bounds();
            bounds.SetMinMax(min, max);

            return bounds;
        }


        public static Bounds GetOrthographicBounds(this GameObject gameObject, Camera camera)
        {
            Vector3 minScreenPosition = Vector3.positiveInfinity;
            Vector3 maxScreenPosition = Vector3.negativeInfinity;

            MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();
            
            foreach (MeshFilter meshFilter in meshFilters)
            {
                if (!meshFilter.sharedMesh)
                {
                    continue;
                }
                
                Vector3[] vertices = meshFilter.sharedMesh.vertices;

                foreach (Vector3 vertex in vertices)
                {
                    Vector3 wsVertexPosition = meshFilter.transform.TransformPoint(vertex);
                    Vector3 screenPosition = camera.WorldToScreenPoint(wsVertexPosition);

                    for (int i = 0; i < 3; i++)
                    {
                        minScreenPosition[i] = Mathf.Min(minScreenPosition[i], screenPosition[i]);
                        maxScreenPosition[i] = Mathf.Max(maxScreenPosition[i], screenPosition[i]);
                    }
                }
            }
            
            Vector3 min = camera.ScreenToWorldPoint(minScreenPosition);
            Vector3 max = camera.ScreenToWorldPoint(maxScreenPosition);

            Bounds bounds = new Bounds();
            bounds.SetMinMax(min, max);
            
            return bounds;
        }


        public static string GetFullName (this GameObject gameObject) {
            StringBuilder nameBuilder = new StringBuilder(gameObject.name);
            
            while (gameObject.transform.parent != null) {
                gameObject = gameObject.transform.parent.gameObject;
                nameBuilder.AppendJoin('/', gameObject.name);
            }
            
            return nameBuilder.ToString();
        }
    }
}