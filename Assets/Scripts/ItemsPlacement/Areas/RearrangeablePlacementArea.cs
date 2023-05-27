using System.Linq;
using GenesisConstructa.ItemsPlacement.Collections;
using GenesisConstructa.ItemsPlacement.Core;
using GenesisConstructa.ItemsPlacement.Core.Area;
using UnityEngine;

namespace GenesisConstructa.ItemsPlacement.Areas
{
    public sealed class RearrangeablePlacementArea : PlacementArea
    {
        protected override void InitializeItemsCollection()
        {
            itemsCollection = new RearrangeablePlacementItemsCollection(placementSettings.MaxItems);
        }


        public override void Remove(PlacementItem item)
        {
            base.Remove(item);

            RearrangeItems();
        }


        private void RearrangeItems()
        {
            PlacementItem[] items = itemsCollection.Items.ToArray();

            for (int i = items.Length - itemsCollection.FirstNullIndex; i < items.Length; i++)
            {
                PlacementItem item = items[i];
                if (item == null)
                {
                    continue;
                }

                Vector3 position = itemPositionCalculator.Calculate(i);
                item.MoveLocally(position);
            }
        }
    }
}