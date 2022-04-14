using System;
using MoonPioneerClone.ItemsRequirementsSystem;
using MoonPioneerClone.UnlockingSystem;
using MoonPioneerClone.Utility.Validating;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.UpgradingSystem
{
    [Serializable]
    public class Upgrade<TUpgradeData> : Unlockable, IValidatable
        where TUpgradeData : UpgradeData
    {
        [TableColumnWidth(200)]
        [SerializeField] private ItemsRequirementsBlock price;
        [TableColumnWidth(160, false)]
        [LabelWidth(100)]
        [SerializeField] private TUpgradeData data;

        public TUpgradeData Data => data;
        public ItemsRequirementsBlock Price => price;

        public event Action<Upgrade<TUpgradeData>> Purchased;


        public void OnValidate()
        {
            price.Satisfied += InvokePurchased;
        }
        

        private void InvokePurchased()
        {
            price.Satisfied -= InvokePurchased;
            
            Purchased?.Invoke(this);
        }
    }
}