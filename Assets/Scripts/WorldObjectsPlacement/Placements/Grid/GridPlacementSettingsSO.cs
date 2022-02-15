using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement.Placements.Grid
{
    [CreateAssetMenu(fileName = "Grid_Placement_Settings", menuName = "World Placement Settings/Grid")]
    public sealed class GridPlacementSettingsSO : WorldPlacementSettingsSO
    {
        [SerializeField] private ItemsAlignment itemsAlignment;

        [SerializeField] private GridPlacementSizeSettings sizeSettings;

        public Vector3 Size { get; private set; }
        public Vector3 ScaledSize { get; private set; }
        public Quaternion Rotation { get; private set; }
        public Vector3 RotatedItemSize { get; private set; }


        public void Resize()
        {
            SetNextSize();
            SetMaxItems(Size);
        }


        private void SetNextSize()
        {
            Size = sizeSettings.GetNextSize();
        }


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
            SetMaxItems(sizeSettings.DefaultSize);
        }


        private void SetMaxItems(Vector3 size)
        {
            MaxItems = (int) (size.x * size.y * size.z);
        }


        private void SetSize()
        {
            Size = sizeSettings.DefaultSize;
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