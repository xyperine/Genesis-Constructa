using ColonizationMobileGame.ItemsPlacement.Core;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.ObjectPooling;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions
{
    [RequireComponent(typeof(PlacementItem))]
    public sealed class StackZoneItem : MonoBehaviour, IPoolable
    {
        [SerializeField] private ItemType type;

        private PlacementItem _placementBehavior;
        private ItemsPool _pool;

        public bool Moving => _placementBehavior.Moving;
        public ItemType Type => type;
        public StackZone Zone { get; private set; }


        private void Awake()
        {
            _placementBehavior = GetComponent<PlacementItem>();
        }


        public void SetZone(StackZone zone)
        {
            Zone = zone;
        }


        public void SetFree()
        {
            if (Zone)
            {
                Zone.Remove(this);
            }
        }


        public void SetPool(ItemsPool pool)
        {
            _pool = pool;
        }


        public void Return()
        {
            if (!_pool)
            {
                SetFree();
                Destroy(gameObject);
                
                Debug.Log("Item was destroyed because it was not created by pool");
                return;
            }
            
            _pool.ReturnObject(this);
        }
    }
}