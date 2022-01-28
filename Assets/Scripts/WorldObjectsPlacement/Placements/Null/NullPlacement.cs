using MoonPioneerClone.WorldObjectsPlacement.Collections;
using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement.Placements.Null
{
    public sealed class NullPlacement : WorldPlacementArea<NullPlacementSettingsSO>
    {
        [SerializeField] private Transform pointTransform;

        public override bool CanFitMore => true;


        protected override void InitializeItemsKeepingBehaviour()
        {
            itemsCollection = new NullPlacementItemsCollection();
        }


        protected override void MoveItemInside(WorldPlacementItem item)
        {
            base.MoveItemInside(item);
            
            Destroy(item.gameObject, item.TransformationsDuration + 0.02f);
        }


        protected override Vector3 CalculatePositionForNewItem()
        {
            return transform.InverseTransformPoint(pointTransform.position);
        }
    }
}