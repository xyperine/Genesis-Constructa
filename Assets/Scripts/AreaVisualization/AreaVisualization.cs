using ColonizationMobileGame.ItemsPlacement.Core.Area;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using ColonizationMobileGame.Utility;
using Shapes;
using Sirenix.OdinInspector;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ColonizationMobileGame.AreaVisualizationNS
{
    [RequireComponent(typeof(Rectangle))]
    public class AreaVisualization : MonoBehaviour
    {
        [BoxGroup("Settings")][SerializeField] private AreaVisualizationSettingsSO settingsSO;
        [BoxGroup("Settings")][SerializeField] private bool overrideSettings;
        [BoxGroup("Settings")] [SerializeField, ShowIf(nameof(overrideSettings)), HideLabel]
        private AreaVisualizationSettingsData customSettings;
        
        [PropertySpace]
        [SerializeField] private PlacementArea placementArea;
        [SerializeField] private Rectangle areaRectangle;
        
        private AreaVisualizationSettingsData _settings;

        private PlacementAreaUpgradeableProperties _upgradeableProperties;
        private float _padding;

        
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
        }


        private void SetSettings()
        {
            _settings = overrideSettings ? customSettings : settingsSO.Data;
        }


        private void ApplySettingsToRectangle()
        {
            areaRectangle.ZOffsetFactor = _settings.DepthOffsetFactor;
            areaRectangle.Color = _settings.Color;
        }


        private void Start()
        {
            SetData();
            SetPositionAndRotation();
        }


        private void SetData()
        {
            _padding = 1 + _settings.ProportionalPadding;
            _upgradeableProperties = placementArea.GetUpgradeableData();
        }


        private void SetPositionAndRotation()
        {
            Vector2 areaSize = _upgradeableProperties.ScaledAreaSize.XZPlaneVector2();
            
            transform.localPosition = areaSize.XZPlaneToVector3() * 0.5f;
            
            if (areaSize.x <= areaSize.y)
            {
                return;
            }

            Vector3 localRotation = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, 90f);
        }


        private void Update()
        {
            SetSize();
        }


        private void SetSize()
        {
            Vector2 areaSize = _upgradeableProperties.ScaledAreaSize.XZPlaneVector2();
            
            areaRectangle.Height = areaSize.x * _padding;
            areaRectangle.Width = areaSize.y * _padding;

            areaRectangle.CornerRadius = Mathf.Min(areaSize.x, areaSize.y) * _settings.CornerRadius;
        }
    }
}