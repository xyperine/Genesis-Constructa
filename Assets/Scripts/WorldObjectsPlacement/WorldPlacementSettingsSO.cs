using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement
{
    public abstract class WorldPlacementSettingsSO : ScriptableObject
    {
        [SerializeField] private Vector3 defaultItemSize = new Vector3(1f, 0.5f, 0.5f);
        [SerializeField] private bool keepItems;

        [Header("Filling")] 
        [SerializeField] private bool useCustomFillingOrder;
        [ShowIf(nameof(useCustomFillingOrder))]
        [ListDrawerSettings(DraggableItems = true, HideAddButton = true, HideRemoveButton = true)]
        [SerializeField, Tooltip("Do not change any value, just rearrange elements instead!\nDefault order is X -> Z -> Y")]
        private WorldPlacementAreaAxesFillingOrder[] fillingOrder = DefaultFillingOrder.ToArray();
        
        private static readonly HashSet<WorldPlacementAreaAxesFillingOrder> DefaultFillingOrder = new HashSet<WorldPlacementAreaAxesFillingOrder>
        {
            WorldPlacementAreaAxesFillingOrder.X,
            WorldPlacementAreaAxesFillingOrder.Z,
            WorldPlacementAreaAxesFillingOrder.Y,
        };

        public Vector3 DefaultItemSize => defaultItemSize;
        public bool KeepItems => keepItems;
        
        public WorldPlacementAreaAxesFillingOrder[] FillingOrder { get; private set; }
        public int MaxItems { get; protected set; }


        [ShowIf(nameof(useCustomFillingOrder))]
        [HorizontalGroup(Width = 140f)]
        [Button(ButtonSizes.Medium)]
        private void ResetFillingOrder()
        {
            fillingOrder = DefaultFillingOrder.ToArray();
        }
        
        
        private void OnValidate()
        {
            CacheSomeValues();
        }


        protected virtual void CacheSomeValues()
        {
            SetFillingOrder();
            SetMaxItems();
            
            if (MaxItems < 0)
            {
                throw new ArgumentOutOfRangeException($"{ nameof(MaxItems) } cannot be less than 0!");
            }
        }


        private void SetFillingOrder()
        {
            WorldPlacementAreaAxesFillingOrder[] order = useCustomFillingOrder ? fillingOrder : DefaultFillingOrder.ToArray();
            FillingOrder = order;
        }

        
        protected abstract void SetMaxItems();
    }
}