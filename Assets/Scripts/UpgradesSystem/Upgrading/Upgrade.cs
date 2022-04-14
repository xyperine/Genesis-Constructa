using System;
using MoonPioneerClone.ItemsRequirementsSystem;
using MoonPioneerClone.UpgradesSystem.Unlocking;
using MoonPioneerClone.Utility.Observing;
using MoonPioneerClone.Utility.Validating;
using UnityEngine;

namespace MoonPioneerClone.UpgradesSystem.Upgrading
{
    [Serializable]
    public class Upgrade<TUpgradeData> : Unlockable, IObservable, IValidatable
        where TUpgradeData : UpgradeData
    {
        [SerializeField] private TUpgradeData data;
        [SerializeField] private ItemsRequirementsBlock price;

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