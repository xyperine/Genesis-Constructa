using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement
{
    public abstract class WorldPlacementAreaShapeDrawer<TPlacementSettings> : MonoBehaviour
        where TPlacementSettings : WorldPlacementSettingsSO
    {
        [SerializeField] protected TPlacementSettings placementSettings;
        [SerializeField] private bool alwaysDraw;


        private void OnValidate()
        {
            RecalculateShape();
        }
        
        
        private void OnDrawGizmosSelected()
        {
            if (!alwaysDraw)
            {
                DrawShape();
            }
        }


        private void OnDrawGizmos()
        {
            if (alwaysDraw)
            {
                DrawShape();
            }
        }


        protected abstract void RecalculateShape();
        protected abstract void DrawShape();
    }
}