using System;
using ColonizationMobileGame.ItemsRequirementsSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.UnlockingSystem
{
    [Serializable]
    public class Unlock
    {
        [HideLabel]
        [SerializeField] private UnlockIdentifier identifier;
        [HideLabel]
        [PropertySpace(16)]
        [SerializeField] private ItemsRequirementsBlock price = new ItemsRequirementsBlock();
        
        public UnlockIdentifier Identifier => identifier;
        public ItemsRequirementsBlock Price => price;
        

        public Unlock(UnlockIdentifier identifier)
        {
            this.identifier = identifier;
        }
    }
}