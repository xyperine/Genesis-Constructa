using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GenesisConstructa.ItemsPlacement.Core.Area
{
    [CreateAssetMenu(fileName = "Placement_Area_Settings", menuName = "Items Placement/Placement Area Settings")]
    public sealed class PlacementAreaSettingsSO : ScriptableObject
    {
        [SerializeField] private Vector3 itemDimensions = new Vector3(0.5f, 1f, 0.5f);

        [Header("Filling")] 
        [SerializeField] private bool useCustomFillingOrder;
        
        [ShowIf(nameof(useCustomFillingOrder))]
        [ListDrawerSettings(DraggableItems = true, HideAddButton = true, HideRemoveButton = true)]
        [Tooltip("Do not change any value, just rearrange elements instead!\nDefault order is X -> Z -> Y")]
        [SerializeField] private Axes[] fillingOrder = DefaultFillingOrder.ToArray();
        
        [ShowIf(nameof(useCustomFillingOrder))]
        [HorizontalGroup(Width = 140f)]
        [Button(ButtonSizes.Medium)]
        private void ResetFillingOrder()
        {
            fillingOrder = DefaultFillingOrder.ToArray();
        }
        
        [SerializeField] private PlacementAlignment alignment;
        [SerializeField] private Vector3Int areaSize;

        private static readonly HashSet<Axes> DefaultFillingOrder =
            new HashSet<Axes>
            {
                Axes.X,
                Axes.Z,
                Axes.Y,
            };

        public Axes[] FillingOrder { get; private set; }
        public int MaxItems { get; private set; }
        
        public Vector3 AreaSize { get; private set; }
        public Quaternion ItemRotation { get; private set; }

        public Vector3 ItemDimensions => itemDimensions;
        public PlacementAlignment Alignment => alignment;

        
        private void OnValidate()
        {
            SetValues();
        }
        
        
#if !UNITY_EDITOR
        private void OnEnable()
        {
            SetValues();
        }
#endif
        

        private void SetValues()
        {
            AreaSize = areaSize;
            
            SetFillingOrder();
            SetMaxItems();
            SetRotation();
        }


        private void SetFillingOrder()
        {
            Axes[] order = useCustomFillingOrder ? fillingOrder : DefaultFillingOrder.ToArray();
            FillingOrder = order;
        }


        private void SetMaxItems()
        {
            MaxItems = areaSize.x * areaSize.y * areaSize.z;
        }


        private void SetRotation()
        {
            ItemRotation = alignment switch
            {
                PlacementAlignment.Origin => Quaternion.identity,
                PlacementAlignment.ForceHorizontal => Quaternion.AngleAxis(-90f, Vector3.forward),
                PlacementAlignment.ForceVertical => Quaternion.AngleAxis(90f, Vector3.forward),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }
}