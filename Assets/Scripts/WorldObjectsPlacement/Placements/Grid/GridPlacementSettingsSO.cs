using System;
using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement.Placements.Grid
{
    [CreateAssetMenu(fileName = "Grid_Placement_Settings", menuName = "World Placement Settings/Grid")]
    public sealed class GridPlacementSettingsSO : WorldPlacementSettingsSO
    {
        [Header("Size (in items)")]
        [SerializeField, Range(1, 30)] private int width;
        [SerializeField, Range(1, 30)] private int height;
        [SerializeField, Range(1, 30)] private int depth;

        [SerializeField] private ItemsAlignment itemsAlignment;

        public Vector3 Size { get; private set; }
        public Vector3 ScaledSize { get; private set; }
        public Quaternion Rotation { get; private set; }
        public Vector3 RotatedItemSize { get; private set; }


        protected override void SetValues()
        {
            base.SetValues();
            
            SetSize();
            SetRotation();
            SetRotatedItemSize();
            SetScaledSize();
        }


        protected override void SetMaxItems()
        {
            MaxItems = width * height * depth;
        }
        
        
        private void SetSize()
        {
            Size = new Vector3(width, height, depth);
        }


        private void SetScaledSize()
        {
            ScaledSize = Vector3.Scale(Size, RotatedItemSize);
        }


        private void SetRotation()
        {
            Rotation = itemsAlignment switch
            {
                ItemsAlignment.Horizontal => Quaternion.identity,
                ItemsAlignment.Vertical => Quaternion.AngleAxis(90f, Vector3.forward),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }


        private void SetRotatedItemSize()
        {
            RotatedItemSize = itemsAlignment switch
            {
                ItemsAlignment.Horizontal => ItemSize,
                ItemsAlignment.Vertical => new Vector3(ItemSize.y, ItemSize.x, ItemSize.z),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }
}