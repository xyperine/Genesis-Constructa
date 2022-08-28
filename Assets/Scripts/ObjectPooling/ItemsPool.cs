using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.SaveLoadSystem;
using UnityEngine;

namespace ColonizationMobileGame.ObjectPooling
{
    public class ItemsPool : MonoBehaviour, ISaveableWithGuid
    {
        [SerializeField] private ItemPoolEntry[] prefabs;
        [SerializeField, Range(100, 500)] private int initialCount = 100;
        [SerializeField] private bool prewarm = true; 

        [SerializeField, HideInInspector] private PermanentGuid guid;

        private readonly List<StackZoneItem> _allItems = new List<StackZoneItem>();
        private readonly List<StackZoneItem> _freeItems = new List<StackZoneItem>();

        private ItemsDistributor _itemsDistributor;
        
        public PermanentGuid Guid => guid;

        
        private void Awake()
        {
            _itemsDistributor = new ItemsDistributor(this);
            
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
            
            _allItems.Add(obj);
            AddObject(obj);
        }


        private void AddObject(StackZoneItem obj)
        {
            obj.gameObject.SetActive(false);
            _freeItems.Add(obj);
        }


        public StackZoneItem Get(ItemType type, Vector3 initialPosition)
        {
            if (!_freeItems.Any(o => o.Type == type && !o.gameObject.activeSelf))
            {
                InstantiateAdditionalObjects(type);
            }

            StackZoneItem item = _freeItems.FindLast(o => o.Type == type);
            item.transform.position = initialPosition;
            item.gameObject.SetActive(true);

            _freeItems.Remove(item);
            
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
            Transform itemTransform = item.transform;
            Transform prefabTransform = prefabs.Single(e => e.ItemPrefab.Type == item.Type).ItemPrefab.transform;
            
            itemTransform.SetParent(transform);
            itemTransform.localPosition = prefabTransform.localPosition;
            itemTransform.localRotation = prefabTransform.localRotation;
        }


        public object Save()
        {
            StackZoneItem[] notFreeItems = _allItems.Where(i => i.Zone != null).ToArray();
            string[] stackZoneGuids = notFreeItems.Select(i => i.Zone.Guid.Value).Distinct().ToArray();

            Dictionary<string, ItemType[]> itemsInStackZones = stackZoneGuids.ToDictionary(g => g,
                g => notFreeItems.Where(i => i.Zone.Guid.Value == g)?.Select(i => i.Type).ToArray());

            return new SaveData
            {
                ItemsInStackZones = itemsInStackZones,
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;
            
            _itemsDistributor.Distribute(saveData.ItemsInStackZones);
        }


        [Serializable]
        private struct SaveData
        {
            public Dictionary<string, ItemType[]> ItemsInStackZones { get; set; }
        }
    }
}