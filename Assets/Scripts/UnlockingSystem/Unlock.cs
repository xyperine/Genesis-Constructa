using System;
using ColonizationMobileGame.ItemsRequirementsSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.UnlockingSystem
{
    [Serializable]
    public class Unlock : IIdentifiable
    {
        [HideLabel]
        [SerializeField] private StructureIdentifier identifier;
        [HideLabel]
        [PropertySpace(16)]
        [SerializeField] private ItemsRequirementsBlock price = new ItemsRequirementsBlock();
        
        public StructureIdentifier Identifier => identifier;
        public ItemsRequirementsBlock Price => price;
        

        public Unlock(StructureIdentifier identifier)
        {
            this.identifier = identifier;
        }
    }
}