using System;
using ColonizationMobileGame.ItemsRequirementsSystem;
using ColonizationMobileGame.UnlockingSystem;
using ColonizationMobileGame.Utility;
using ColonizationMobileGame.Utility.Validating;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.UpgradingSystem
{
    [Serializable]
    public class Upgrade<TUpgradeData> : Unlockable, IValidatable, IDeepCloneable<Upgrade<TUpgradeData>>
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


        public Upgrade<TUpgradeData> GetDeepCopy()
        {
            Upgrade<TUpgradeData> copy = new Upgrade<TUpgradeData>
            {
                price = price.GetDeepCopy(),
                data = data,
            };
            
            copy.WirePurchasedEventToPrice();

            return copy;
        }
        
        
        public void OnValidate()
        {
            WirePurchasedEventToPrice();
        }


        private void WirePurchasedEventToPrice()
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