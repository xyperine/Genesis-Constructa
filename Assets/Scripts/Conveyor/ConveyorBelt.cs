using System;
using System.Collections.Generic;
using GenesisConstructa.Items;
using GenesisConstructa.ItemsPlacementsInteractions;
using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;
using GenesisConstructa.ItemsPlacementsInteractions.Target;
using UnityEngine;

namespace GenesisConstructa.Conveyor
{
    public class ConveyorBelt : InteractionTarget
    {
        [Header("References")]
        [SerializeField] private ConveyorSpeedController speedController; 
        [SerializeField] private ConveyorPowerController powerController;
        
        [SerializeField] private StackZone productionZone;

        [Header("Belt Points")]
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform unlockPoint;
        [SerializeField] private Transform endPoint;
        
        private readonly List<StackZoneItem> _itemsOnBelt = new List<StackZoneItem>();
        private readonly Vector3 _direction = Vector3.back;

        public override bool CanTakeMore => productionZone.CanTakeMore;
        public override ItemType[] AcceptableItems { get; } = (ItemType[]) Enum.GetValues(typeof(ItemType));


        public override void Add(StackZoneItem item)
        {
            if (_itemsOnBelt.Contains(item))
            {
                return;
            }
            
            item.LockForPlayer(true);
            
            item.SetFree();
            item.SetZone(null);
            
            item.transform.SetParent(transform);
            item.transform.localPosition = startPoint.localPosition;
            _itemsOnBelt.Add(item);
        }


        private void Update()
        {
            if (!powerController.Powered)
            {
                return;
            }
            
            MoveItems();
        }


        private void MoveItems()
        {
            for (int i = _itemsOnBelt.Count - 1; i >= 0; i--)
            {
                StackZoneItem item = _itemsOnBelt[i];
                if (item.Moving)
                {
                    continue;
                }

                if (item.transform.localPosition.z <= unlockPoint.localPosition.z)
                {
                    item.LockForPlayer(false);
                }

                if (item.Zone)
                {
                    _itemsOnBelt.Remove(item);
                    continue;
                }

                if (item.transform.localPosition.z <= endPoint.localPosition.z)
                {
                    MoveItemToProductionZone(item);
                    continue;
                }

                MoveItem(item);
            }
        }
        
        
        private void MoveItem(StackZoneItem item)
        {
            Vector3 movement = speedController.Speed * Time.deltaTime * _direction;
            item.transform.localPosition += movement;
        }
        
        
        private void MoveItemToProductionZone(StackZoneItem item)
        {
            item.LockForPlayer(false);

            _itemsOnBelt.Remove(item);

            productionZone.Add(item);
        }


        private void OnDrawGizmos()
        {
            const float radius = 0.15f;

            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(startPoint.position, radius);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(unlockPoint.position, radius);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(endPoint.position, radius);
        }
    }
}