using Sirenix.OdinInspector;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ColonizationMobileGame.AreaVisualizationNS
{
    [CreateAssetMenu(fileName = "Area_Visualization_Settings", menuName = "Area Visualization Settings", order = 0)]
    public class AreaVisualizationSettingsSO : ScriptableObject
    {
        [SerializeField, HideLabel] private AreaVisualizationSettingsData data;

        public AreaVisualizationSettingsData Data => data;
    }
}