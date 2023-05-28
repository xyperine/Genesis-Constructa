using System.Collections.Generic;
using System.Linq;
using GenesisConstructa.ItemsPlacement.Collections;
using GenesisConstructa.ItemsPlacement.Core;
using GenesisConstructa.ItemsPlacement.Core.Area;
using UnityEngine;

namespace GenesisConstructa.ItemsPlacement.Areas
{
    public sealed class RearrangeablePlacementArea : PlacementArea
    {
        private readonly Dictionary<PlacementItem, int> _movingItemsIndices = new Dictionary<PlacementItem, int>();


        protected override void InitializeItemsCollection()
        {
            itemsCollection = new RearrangeablePlacementItemsCollection(placementSettings.MaxItems);
        }


        protected override void MoveItem(PlacementItem item)
        {
            _movingItemsIndices.Add(item, itemsCollection.FirstNullIndex);
            item.Arrived += OnArrival;
            void OnArrival()
            {
                AdjustPosition(item);
                item.Arrived -= OnArrival;
                _movingItemsIndices.Remove(item);
            };
            
            base.MoveItem(item);
        }


        private void AdjustPosition(PlacementItem item)
        {
            if (_movingItemsIndices[item] > itemsCollection.FirstNullIndex)
            {
                RearrangeItems();
            }
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