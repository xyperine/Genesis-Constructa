using System;
using ColonizationMobileGame.AreaVisualizationNS.Transformers;
using ColonizationMobileGame.BuildSystem;
using ColonizationMobileGame.ItemsPlacement.Core.Area;
using ColonizationMobileGame.ItemsPlacementsInteractions;
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
        [SerializeField, ShowIf(nameof(TargetIsStartItems))] private Transform itemsHolder;
        
        [SerializeField] private Rectangle areaRectangle;
        
        private AreaVisualizationSettingsData _settings;

        private AreaVisualizationTransformer _transformer;

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
            
            CreateTransformer();
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


        private void CreateTransformer()
        {
            _transformer = _settings.Target switch
            {
                AreaVisualizationTarget.PlacementArea => new PlacementAreaVisualizationTransformer(areaRectangle,
                    _settings, placementArea),
                AreaVisualizationTarget.Builder => new BuilderAreaVisualizationTransformer(areaRectangle, _settings,
                    builder),
                AreaVisualizationTarget.StartItems => new StartItemsAreaVisualizationTransformer(areaRectangle,
                    _settings, itemsHolder),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }


        private void Start()
        {
            _transformer.PerformTransformations(transform);
            
            _transformer.SetSize();
        }
    }
}