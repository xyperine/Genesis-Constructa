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


        protected override void MoveItemInside(WorldPlacementItem item)
        {
            Vector3 position = GetPositionForNewItem();
            
            item.transform.SetParent(transform);
            item.Move(position);
            
            Destroy(item.gameObject, item.TransformationsDuration + 0.02f);
        }


        protected override Vector3 GetPositionForNewItem()
        {
            return transform.InverseTransformPoint(pointTransform.position);
        }
    }
}