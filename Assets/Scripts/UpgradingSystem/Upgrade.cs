using System;
using MoonPioneerClone.ItemsRequirementsSystem;
using MoonPioneerClone.UnlockingSystem;
using MoonPioneerClone.Utility.Observing;
using MoonPioneerClone.Utility.Validating;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.UpgradingSystem
{
    [Serializable]
    public class Upgrade<TUpgradeData> : Unlockable, IObservable, IValidatable
        where TUpgradeData : UpgradeData
    {
        [TableColumnWidth(200)]
        [SerializeField] private ItemsRequirementsBlock price;
        [TableColumnWidth(160, false)]
        [LabelWidth(100)]
        [SerializeField] private TUpgradeData data;

        public TUpgradeData Data => data;
        public ItemsRequirementsBlock Price => price;
        public bool Satisfied { get; private set; }
        
        public event Action Changed;


        public override void Unlock()
        {
            base.Unlock();
            
            Changed?.Invoke();
        }
        
        
        public void OnValidate()
        {
            price.Satisfied += InvokeChangedDueToBeingPurchased;
            Satisfied = false;
        }
        

        private void InvokeChangedDueToBeingPurchased()
        { 
            Satisfied = true;
            price.Satisfied -= InvokeChangedDueToBeingPurchased;
            
            Changed?.Invoke();
        }
    }
}