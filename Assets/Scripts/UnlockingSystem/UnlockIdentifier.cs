using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.UnlockingSystem
{
    [Serializable]
    public class UnlockIdentifier
    {
        [LabelWidth(150)]
        [SerializeField, ReadOnly] private StructureType structureType;
        [LabelWidth(150)]
        [SerializeField, ReadOnly] private int level;

        public StructureType StructureType => structureType;
        public int Level => level;


        public UnlockIdentifier(StructureType structureType, int level)
        {
            this.structureType = structureType;
            this.level = level;
        }


        public override bool Equals(object obj)
        {
            UnlockIdentifier identifier = (UnlockIdentifier) obj;
            return structureType == identifier.structureType && level == identifier.level;
        }


        public override int GetHashCode()
        {
            return HashCode.Combine((int) structureType, level);
        }
    }
}