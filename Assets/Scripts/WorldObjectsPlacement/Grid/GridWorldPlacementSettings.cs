using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement.Grid
{
    [CreateAssetMenu(fileName = "GridWorldPlacementSettings", menuName = "World Placement Settings/Grid")]
    public class GridWorldPlacementSettings : WorldPlacementSettingsSO
    {
        [Header("Size (in items)")]
        [SerializeField, Range(1, 30)] private int width;
        [SerializeField, Range(1, 30)] private int height;
        [SerializeField, Range(1, 30)] private int depth;
        
        public int Width => width;
        public int Height => height;
        public int Depth => depth;
        
        public Vector3 Size { get; private set; }


        protected override void CacheSomeValues()
        {
            base.CacheSomeValues();
            SetSize();
        }


        protected override void SetMaxItems()
        {
            MaxItems = width * height * depth;
        }
        
        
        private void SetSize()
        {
            Size = new Vector3(width, height, depth);
        }
    }
}