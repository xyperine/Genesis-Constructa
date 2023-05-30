using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GenesisConstructa.Items;
using GenesisConstructa.ItemsExtraction.ConditionsLogic;
using GenesisConstructa.ItemsPlacementsInteractions;
using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;
using GenesisConstructa.ItemsPlacementsInteractions.Target;
using GenesisConstructa.Utility.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GenesisConstructa
{
    public class Conveyor : InteractionTarget
    {
        [Header("References")]
        [SerializeField] private new Renderer renderer;
        [SerializeField] private Material material;
        [SerializeField] private ExtractorConditionsUnit conditionsUnit;
        [SerializeField] private StackZone productionZone;

        [Header("Important points on the belt")]
        [SerializeField] private Transform startingPoint;
        [SerializeField] private Transform unlockPoint;
        [SerializeField] private Transform endPoint;

        [Header("Properties")]
        [SerializeField, Range(-1f, 1f)] private float speed;
        [SerializeField] private bool workAfterGoingOffline;
        [SerializeField, ShowIf(nameof(workAfterGoingOffline)), Min(0f), Indent()] private float turnOffDelayInSeconds;

        private static readonly int SpeedShaderPropertyID = Shader.PropertyToID("_Speed");
        
        private readonly List<StackZoneItem> _itemsOnBelt = new List<StackZoneItem>();
        private readonly Vector3 _direction = Vector3.back;

        private bool _powered;
        private Material _runtimeMaterial;

        public override bool CanTakeMore => productionZone.CanTakeMore;
        public override ItemType[] AcceptableItems { get; } = (ItemType[]) Enum.GetValues(typeof(ItemType));

        
        private void OnValidate()
        {
            SetMaterialSpeed(material, speed);
        }


        private void SetMaterialSpeed(Material material, float speed)
        {
            const float speedSyncMultiplier = 0.182f;
            material.SetFloat(SpeedShaderPropertyID, speed * speedSyncMultiplier);
        }


        private void Awake()
        {
            _runtimeMaterial = renderer.materials.Single(m => m.name[..material.name.Length] == material.name);
            
            conditionsUnit.ConditionsChanged += OnConditionsChanged;
        }


        private void OnConditionsChanged()
        {
            if (conditionsUnit.WorkConditionsMet)
            {
                SetPoweredStatus(true);
                return;
            }
            
            if (workAfterGoingOffline)
            {
                StartCoroutine(DelayTurnOffRoutine());
            }
            else
            {
                SetPoweredStatus(false);
            }
        }


        private IEnumerator DelayTurnOffRoutine()
        {
            yield return YieldInstructionsHelpers.GetWaitForSeconds(turnOffDelayInSeconds);
            
            SetPoweredStatus(false);
        }


        private void SetPoweredStatus(bool status)
        {
            _powered = status;

            float speed = _powered ? 
                this.speed : 0f;
            SetMaterialSpeed(_runtimeMaterial, speed);
        }


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
            item.transform.localPosition = startingPoint.localPosition;
            _itemsOnBelt.Add(item);
        }


        private void Update()
        {
            if (!_powered)
            {
                return;
            }
            
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
                    MoveToProductionZone(item);
                    continue;
                }

                Move(item);
            }
        }


        private void Move(StackZoneItem item)
        {
            Vector3 movement = speed * Time.deltaTime * _direction;
            item.transform.localPosition += movement;
        }
        
        
        private void MoveToProductionZone(StackZoneItem item)
        {
            item.LockForPlayer(false);

            _itemsOnBelt.Remove(item);

            productionZone.Add(item);
        }


        private void OnDrawGizmos()
        {
            const float radius = 0.15f;

            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(startingPoint.position, radius);
            
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(unlockPoint.position, radius);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(endPoint.position, radius);
        }
    }
}