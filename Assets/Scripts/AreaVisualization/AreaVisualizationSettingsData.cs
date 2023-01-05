using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ColonizationMobileGame.AreaVisualizationNS
{
    [Serializable]
    public class AreaVisualizationSettingsData
    {
        [SerializeField] private float depthOffsetFactor = -20f;
        [SerializeField] private Color color;
        [SerializeField, Range(0f, 1f)] private float cornerRadius = 0.15f;
        [SerializeField] private float proportionalPadding = 0.2f;

        public float DepthOffsetFactor => depthOffsetFactor;
        public Color Color => color;
        public float CornerRadius => cornerRadius;
        public float ProportionalPadding => proportionalPadding;
    }
}