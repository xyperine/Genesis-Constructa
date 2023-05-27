using System;
using ColonizationMobileGame.AreaVisualizationNS.TargetFitters;
using GenesisConstructa.BuildSystem;
using GenesisConstructa.ItemsPlacement.Core.Area;
using Shapes;
using Sirenix.OdinInspector;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ColonizationMobileGame.AreaVisualizationNS
{
    [RequireComponent(typeof(Rectangle))]
    public class AreaVisualization : MonoBehaviour
    {
        [BoxGroup("Settings")] [SerializeField] private AreaVisualizationSettingsSO settingsSO;
        [BoxGroup("Settings")] [SerializeField] private bool overrideSettings;
        [BoxGroup("Settings")] [SerializeField, ShowIf(nameof(overrideSettings)), HideLabel]
        private AreaVisualizationSettingsData customSettings;
        
        [PropertySpace]
        [SerializeField, ShowIf(nameof(TargetIsPlacementArea))] private PlacementArea placementArea;
        [SerializeField, ShowIf(nameof(TargetIsBuilder))] private Builder builder;
        [SerializeField, ShowIf(nameof(TargetIsStartItems))] private GameObject itemsHolderObject;
        
        [SerializeField] private Rectangle areaRectangle;
        
        private AreaVisualizationSettingsData _settings;

        private AreaVisualizationTargetFitter _targetFitter;

        private bool TargetIsPlacementArea => _settings.Target == AreaVisualizationTarget.PlacementArea;
        private bool TargetIsBuilder => _settings.Target == AreaVisualizationTarget.Builder;
        private bool TargetIsStartItems => _settings.Target == AreaVisualizationTarget.StartItems;


#if UNITY_EDITOR
        private void OnValidate()
        {
            SetSettings();
            
            ApplySettingsToRectangle();
        }
#endif

        private void Awake()
        {
            SetSettings();
            
            ApplySettingsToRectangle();
            
            CreateFitter();
        }


        private void SetSettings()
        {
            _settings = overrideSettings ? customSettings : settingsSO.Data;
        }


        private void ApplySettingsToRectangle()
        {
            areaRectangle.ZOffsetFactor = _settings.DepthOffsetFactor;
            areaRectangle.Color = _settings.Color;

            areaRectangle.Type = Rectangle.RectangleType.RoundedSolid;
        }


        private void CreateFitter()
        {
            _targetFitter = _settings.Target switch
            {
                AreaVisualizationTarget.PlacementArea => new PlacementAreaVisualizationTargetFitter(areaRectangle,
                    _settings, placementArea),
                AreaVisualizationTarget.Builder => new BuilderAreaVisualizationTargetFitter(areaRectangle, _settings,
                    builder),
                AreaVisualizationTarget.StartItems => new StartItemsAreaVisualizationTargetFitter(areaRectangle,
                    _settings, itemsHolderObject),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }


        private void Start()
        {
            _targetFitter.Fit(transform);
        }
    }
}