using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GenesisConstructa.Structures
{
    [Serializable]
    public class StructureIdentifier
    {
        [LabelWidth(150)]
        [SerializeField, ReadOnly] private StructureType structureType;
        [LabelWidth(150)]
        [SerializeField, ReadOnly] private int level;

        public StructureType StructureType => structureType;
        public int Level => level;


        public StructureIdentifier(StructureType structureType, int level)
        {
            this.structureType = structureType;
            this.level = level;
        }


        public override bool Equals(object obj)
        {
            StructureIdentifier identifier = (StructureIdentifier) obj;
            return structureType == identifier.structureType && level == identifier.level;
        }


        public override int GetHashCode()
        {
            return HashCode.Combine((int) structureType, level);
        }
    }
}