using System;
using System.Collections.Generic;
using GenesisConstructa.Structures;
using GenesisConstructa.UnlockingSystem;
using UnityEngine;

namespace GenesisConstructa.UI.ItemsAmount.Data
{
    public class ItemsAmountPanelData : MonoBehaviour
    {
        public IReadOnlyList<ItemAmountData> ItemCounts { get; private set; }
        public StructureIdentifier Identifier { get; private set; }
        public bool Locked { get; private set; }

        public event Action Changed;
        public event Action Unlocked;


        public void SetData(ItemAmountData data)
        {
            SetData(new[] {data});
        }


        public void SetData(ItemAmountData[] itemCounts)
        {
            ItemCounts = itemCounts;
        }


        public void SetIdentifier(StructureIdentifier identifier)
        {
            Identifier = identifier;
        }


        public void SetUnlockable(IUnlockable unlockable)
        {
            if (unlockable == null)
            {
                return;
            }
            
            Locked = unlockable.Locked;
            
            if (Locked)
            {
                unlockable.Unlocked += OnUnlocked;
            }
        }


        private void OnUnlocked()
        {
            Locked = false;
            Unlocked?.Invoke();
        }


        public void InvokeChanged()
        {
            Changed?.Invoke();
        }
    }
}