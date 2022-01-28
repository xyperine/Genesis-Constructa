using System.Linq;
using MoonPioneerClone.WorldObjectsPlacement;
using MoonPioneerClone.WorldObjectsPlacement.Collections;
using MoonPioneerClone.WorldObjectsPlacement.Placements.Grid;
using UnityEngine;

namespace MoonPioneerClone.Player
{
    public class PlayerGridPlacement : GridPlacement
    {
        protected override void InitializeItemsKeepingBehaviour()
        {
            itemsCollection = new RearrangeablePlacementItemsCollection(placementSettings.MaxItems);
        }


        public override void Remove(WorldPlacementItem item)
        {
            base.Remove(item);

            RearrangeItems();
        }


        private void RearrangeItems()
        {
            WorldPlacementItem[] items = itemsCollection.Items.ToArray();

            for (int i = items.Length - itemsCollection.FirstNullIndex; i < items.Length; i++)
            {
                WorldPlacementItem item = items[i];
                if (item == null)
                {
                    continue;
                }

                Vector3 position = CalculatePositionForNewItem(i);
                item.MoveLocally(position);
            }
        }
    }
}