using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement.OnePoint
{
    public sealed class OnePointPlacement : WorldPlacementArea<OnePointWorldPlacementSettings>
    {
        [SerializeField] private Transform pointTransform;

        public override bool CanFitMore => true;


        protected override void InitializeItemsKeepingBehaviour()
        {
            itemsKeepingBehaviour = new OnePointItemsKeepingBehaviour();
        }


        protected override Vector3 GetPositionForNewItem()
        {
            return transform.InverseTransformPoint(pointTransform.position);
        }
    }
}