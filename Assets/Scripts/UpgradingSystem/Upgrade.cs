using System;
using ColonizationMobileGame.ItemsRequirementsSystem;
using ColonizationMobileGame.Structures;
using ColonizationMobileGame.UnlockingSystem;
using ColonizationMobileGame.Utility;
using ColonizationMobileGame.Utility.Validating;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace ColonizationMobileGame.UpgradingSystem
{
    [Serializable]
    public class Upgrade<TUpgradeData> : IUnlockable, IValidatable, IDeepCloneable<Upgrade<TUpgradeData>>
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
       
        private bool _defaultLockedState;
        
        public bool Locked => locked;
        public TUpgradeData Data => data;

        public ItemRequirementsBlock Price => price;

        public StructureIdentifier Identifier
        {
            get => identifier;
            set => identifier = value;
        }

        public event Action Unlocked;
        public event Action<Upgrade<TUpgradeData>> Purchased;

        
        public void Unlock()
        {
            _defaultLockedState = locked;
            
            locked = false;
            price.Locked = false;
            Unlocked?.Invoke();
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += ResetLockedState;
#endif
        }


#if UNITY_EDITOR
        private void ResetLockedState(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                locked = _defaultLockedState;
            }
        }
#endif
        

        public Upgrade<TUpgradeData> GetDeepCopy()
        {
            Upgrade<TUpgradeData> copy = new Upgrade<TUpgradeData>
            {
                price = price.GetDeepCopy(),
                data = data,
                identifier = identifier,
                locked = locked,
            };

            copy.price.Locked = locked;
            copy.WirePurchasedEventToPrice();
            
            Unlocked += copy.Unlock;

            return copy;
        }
        
        
        public void OnValidate()
        {
            price.Locked = locked;
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
    }
}