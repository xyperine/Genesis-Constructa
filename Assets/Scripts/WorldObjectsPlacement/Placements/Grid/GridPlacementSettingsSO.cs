using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement.Placements.Grid
{
    [CreateAssetMenu(fileName = "GridPlacementSettings", menuName = "World Placement Settings/Grid")]
    public sealed class GridPlacementSettingsSO : WorldPlacementSettingsSO
    {
        [Header("Size (in items)")]
        [SerializeField, Range(1, 30)] private int width;
        [SerializeField, Range(1, 30)] private int height;
        [SerializeField, Range(1, 30)] private int depth;

        public Vector3 Size { get; private set; }
        public Vector3 ScaledSize { get; private set; }


        protected override void SetValues()
        {
            base.SetValues();
            
            SetSize();
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
            ScaledSize = Vector3.Scale(Size, ItemSize);
        }
    }
}