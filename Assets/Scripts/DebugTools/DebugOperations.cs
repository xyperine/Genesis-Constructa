using System;
using System.Collections.Generic;
using System.Linq;
using GenesisConstructa.BuildSystem;
using GenesisConstructa.Items;
using GenesisConstructa.ItemsExtraction.Upgrading;
using GenesisConstructa.ItemsPlacementsInteractions;
using GenesisConstructa.ObjectPooling;
using GenesisConstructa.Player;
using GenesisConstructa.Structures;
using GenesisConstructa.UnlockingSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GenesisConstructa.DebugTools
{
    public class DebugOperations : MonoBehaviour
    {
        [PropertySpace(SpaceAfter = 8f)]
        [SerializeField, BoxGroup("Structures")] private UnlocksChainSO unlocksChain;
        
        private List<Unlock> _unlocks;


        private void Start()
        {
            _unlocks = unlocksChain.Unlocks;
        }

        
        [PropertySpace(SpaceAfter = 8f)]
        [Button(DisplayParameters = true), BoxGroup("Structures")]
        public void Unlock(StructureType structureType)
        {
            Unlock unlock = _unlocks.Find(u => u.Identifier.StructureType == structureType);

            if (unlock == null)
            {
                Debug.LogWarning($"No unlocks was found for {structureType} structure!");
            }
            
            unlock?.ForceUnlock();
        }

        
        [PropertySpace(SpaceAfter = 8f)]
        [Button(DisplayParameters = true), BoxGroup("Structures")]
        public void UnlockAndBuild(StructureType structureType)
        {
            Unlock(structureType);
            
            Builder builder = FindObjectsOfType<Builder>().ToList()
                .Find(b => b.Identifier.StructureType == structureType);

            if (builder == null)
            {
                Debug.LogWarning($"No builders for {structureType} was found!");
                return;
            }
            
            builder.Build();
        }

        
        [PropertySpace(SpaceAfter = 8f)]
        [Button(DisplayParameters = true), BoxGroup("Structures")]
        public void Upgrade(StructureType structureType)
        {
            Structure structure = FindObjectsOfType<Structure>().ToList().Find(s => s.Type == structureType);

            if (!structure)
            {
                Debug.LogWarning($"Was unable to find {structureType} structure!");
                return;
            }

            ExtractorUpgrader upgrader = structure.GetComponentInChildren<ExtractorUpgrader>();

            if (!upgrader)
            {
                Debug.LogWarning($"Was unable to find upgrader for {structureType} structure!");
                return;
            }

            ExtractorUpgradeData upgradeData = upgrader.Chain?.Current?.Data;

            if (upgradeData == null)
            {
                Debug.LogWarning($"Was unable to get any upgrade data for {structureType} structure!");
                return;
            }
            
            upgrader.Upgrade(upgradeData);
            
            Debug.Log($"Structure {structureType} was upgraded!");
        }

        
        [PropertySpace(SpaceAfter = 8f)]
        [Button(DisplayParameters = true), BoxGroup("Player")]
        public void GiveItems(Dictionary<ItemType, uint> items)
        {
            ItemsPool pool = FindObjectOfType<ItemsPool>();

            if (!pool)
            {
                Debug.LogWarning("Was unable to find Items Pool!");
                return;
            }

            PlayerStackZone playerStackZone = FindObjectOfType<PlayerStackZone>();
            
            if (!playerStackZone)
            {
                Debug.LogWarning("Was unable to find player's stack zone!");
                return;
            }

            if (!playerStackZone.CanTakeMore)
            {
                Debug.LogWarning("Player's stack zone can't take more items!");
                return;
            }

            foreach ((ItemType itemType, uint count) in items)
            {
                for (int i = 1; i <= count; i++)
                {
                    StackZoneItem item = pool.Get(itemType, Vector3.zero);
                    playerStackZone.Add(item);
                }
            }
        }

        
        [Button, BoxGroup("Player")]
        public void ClearPlayerZone()
        {
            PlayerStackZone playerStackZone = FindObjectOfType<PlayerStackZone>();
            
            if (!playerStackZone)
            {
                Debug.LogWarning("Was unable to find player's stack zone!");
                return;
            }

            while (playerStackZone.HasItems)
            {
                StackZoneItem item = playerStackZone.GetLast((ItemType[]) Enum.GetValues(typeof(ItemType)));
                playerStackZone.Remove(item);
                item.Return();
            }
        }


        [Button(DisplayParameters = true), BoxGroup("Misc")]
        public void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
        }
    }
}