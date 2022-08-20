using System;
using ColonizationMobileGame.ItemsRequirementsSystem;
using ColonizationMobileGame.Structures;
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
        [SerializeField] private ItemRequirementsBlock price = new ItemRequirementsBlock();
        
        public StructureIdentifier Identifier => identifier;
        public ItemRequirementsBlock Price => price;
        

        public Unlock(StructureIdentifier identifier)
        {
            this.identifier = identifier;
        }
    }
}