using System.Collections.Generic;
using MoonPioneerClone.UpgradingSystem;
using UnityEngine;

namespace MoonPioneerClone.UnlockingSystem
{
    [CreateAssetMenu(fileName = "Unlocks_Chain", menuName = "Unlocks Chain", order = 0)]
    public class UnlocksChainSO : ScriptableObject
    {
        [SerializeField] private List<Unlock> unlocks;
        [SerializeField] private UpgradesChainSO<UpgradeData>[] chains;


        private void OnValidate()
        {
            
        }
    }
}