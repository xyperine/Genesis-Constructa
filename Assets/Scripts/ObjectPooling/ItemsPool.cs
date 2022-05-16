using System.Collections.Generic;
using System.Linq;
using MoonPioneerClone.ItemsPlacementsInteractions;
using UnityEngine;

namespace MoonPioneerClone.ObjectPooling
{
    public class ItemsPool : MonoBehaviour
    {
        [SerializeField] private ItemPoolEntry[] prefabs;
        [SerializeField, Range(100, 500)] private int initialCount = 100;
        
        [SerializeField] private bool prewarm = true;

        private readonly List<StackZoneItem> _objects = new List<StackZoneItem>();
        

        private void Awake()
        {
            if (prewarm)
            {
                Prewarm();
            }
        }


        private void Prewarm()
        {
            InstantiateAllObjects(initialCount);
        }


        private void InstantiateAllObjects(int totalCount)
        {
            ItemPoolEntry[] sortedByWeightPrefabs = prefabs.OrderByDescending(e => e.Weight).ToArray();
            float totalWeight = sortedByWeightPrefabs.Sum(e => e.Weight);

            foreach (ItemPoolEntry poolEntry in sortedByWeightPrefabs)
            {
                int count = Mathf.FloorToInt(poolEntry.Weight / totalWeight * totalCount);
                StackZoneItem itemPrefab = poolEntry.ItemPrefab;
                
                for (int j = 0; j < count; j++)
                {
                    InstantiateSingleObject(itemPrefab);
                }
            }
        }


        private void InstantiateSingleObject(StackZoneItem itemPrefab)
        {
            StackZoneItem obj = Instantiate(itemPrefab, transform);
            obj.SetPool(this);
            
            AddObject(obj);
        }


        private void AddObject(StackZoneItem obj)
        {
            obj.gameObject.SetActive(false);
            _objects.Add(obj);
        }


        public StackZoneItem Get(ItemType type, Vector3 initialPosition)
        {
            if (!_objects.Any(o => o.Type == type && !o.gameObject.activeSelf))
            {
                InstantiateAdditionalObjects(type);
            }

            StackZoneItem item = _objects.FindLast(o => o.Type == type);
            item.transform.position = initialPosition;
            item.gameObject.SetActive(true);

            _objects.Remove(item);
            
            return item;
        }


        private void InstantiateAdditionalObjects(ItemType type)
        {
            StackZoneItem prefab = prefabs.Single(e => e.ItemPrefab.Type == type).ItemPrefab;
            
            for (int i = 0; i < 5; i++)
            {
                InstantiateSingleObject(prefab);
            }
        }


        public void ReturnObject(StackZoneItem item)
        {
            ResetTransform(item);
            AddObject(item);
        }


        private void ResetTransform(StackZoneItem item)
        {
            StackZoneItem prefab = prefabs.Single(e => e.ItemPrefab.Type == item.Type).ItemPrefab;

            Transform itemTransform = item.transform;
            
            itemTransform.SetParent(transform);
            itemTransform.localPosition = prefab.transform.localPosition;
            itemTransform.localRotation = prefab.transform.localRotation;
        }
    }
}