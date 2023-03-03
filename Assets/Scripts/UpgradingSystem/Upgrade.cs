using System;
using ColonizationMobileGame.ItemsRequirementsSystem;
using ColonizationMobileGame.Structures;
using ColonizationMobileGame.UnlockingSystem;
using ColonizationMobileGame.Utility;
using ColonizationMobileGame.Utility.Validating;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.UpgradingSystem
{
    [Serializable]
    public class Upgrade<TUpgradeData> : IUnlockable, IValidatable, IDeepCloneable<Upgrade<TUpgradeData>>, ISerializationCallbackReceiver
        where TUpgradeData : UpgradeData
    {
        [TableColumnWidth(64, false)]
        [SerializeField] private bool locked;
        [TableColumnWidth(200)]
        [SerializeField] private ItemRequirementsBlock price;
        [TableColumnWidth(160, false)]
        [LabelWidth(100)]
        [SerializeField] private TUpgradeData data;
        
        [SerializeField, HideInInspector] private StructureIdentifier identifier;
        
        public bool Locked { get; private set; }
        public TUpgradeData Data => data;

        public ItemRequirementsBlock Price => price;

        public StructureIdentifier Identifier
        {
            get => identifier;
            set => identifier = value;
        }

        public event Action Unlocked;
        public event Action<Upgrade<TUpgradeData>> Purchased;


        public void OnValidate()
        {
            Locked = locked;

            price.Locked = Locked;
            WirePurchasedEventToPrice();
        }


        private void WirePurchasedEventToPrice()
        {
            price.Fulfilled += InvokePurchased;
        }
        

        private void InvokePurchased()
        {
            price.Fulfilled -= InvokePurchased;
            
            Purchased?.Invoke(this);
        }


        public void Unlock()
        {
            Locked = false;
            price.Locked = false;
            
            Unlocked?.Invoke();
            
            Debug.Log($"Upgrade for {identifier.StructureType} structure was unlocked!");
        }


        public Upgrade<TUpgradeData> GetDeepCopy()
        {
            Upgrade<TUpgradeData> copy = new Upgrade<TUpgradeData>
            {
                price = price.GetDeepCopy(),
                data = data,
                identifier = identifier,
                Locked = Locked,
            };

            copy.price.Locked = Locked;
            copy.WirePurchasedEventToPrice();
            
            Unlocked += copy.Unlock;

            return copy;
        }


        public void OnBeforeSerialize()
        {
            
        }


        public void OnAfterDeserialize()
        {
            Locked = locked;

            price.Locked = Locked;
            WirePurchasedEventToPrice();
        }
    }
}