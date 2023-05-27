using System;
using GenesisConstructa.ItemsRequirementsSystem;
using GenesisConstructa.Structures;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GenesisConstructa.UnlockingSystem
{
    [Serializable]
    public class Unlock : IIdentifiable
    {
        [HideLabel]
        [SerializeField] private StructureIdentifier identifier;
        [HideLabel]
        [PropertySpace(16)]
        [SerializeField] private ItemRequirementsBlock price = new ItemRequirementsBlock();

        private IUnlockable _unlockable;

        public StructureIdentifier Identifier => identifier;
        public ItemRequirementsBlock Price => price;
        

        public Unlock(StructureIdentifier identifier)
        {
            this.identifier = identifier;
        }


        public void ConnectWith(IUnlockable unlockable)
        {
            if (unlockable == null)
            {
                return;
            }

            _unlockable = unlockable;
            
            price.Fulfilled += _unlockable.Unlock;
        }


        public void ForceUnlock()
        {
            if (_unlockable == null)
            {
                Debug.Log("No unlockable!");
            }
            
            _unlockable?.Unlock();
        }
    }
}