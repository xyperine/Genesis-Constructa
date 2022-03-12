using System;
using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement.Placements.Grid
{
    [CreateAssetMenu(fileName = "Grid_Placement_Settings", menuName = "World Placement Settings/Grid")]
    public sealed class GridPlacementSettingsSO : WorldPlacementSettingsSO
    {
        [SerializeField] private ItemsAlignment itemsAlignment;
        [SerializeField] private Vector3Int size;

        public Vector3 Size { get; private set; }
        public Vector3 ScaledSize { get; private set; }
        public Quaternion Rotation { get; private set; }
        public Vector3 RotatedItemSize { get; private set; }


        public void Grow(int newMaxItems)
        {
            MaxItems = newMaxItems;
            
            float itemsPerY = Size.x * Size.z;
            Size = new Vector3(Size.x, Mathf.Ceil(newMaxItems / itemsPerY), Size.z);
        }


        protected override void SetValues()
        {
            base.SetValues();

            Size = size;
            SetRotation();
            SetRotatedItemSize();
            SetScaledSize();
        }


        protected override void SetMaxItems()
        {
            MaxItems = size.x * size.y * size.z;

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