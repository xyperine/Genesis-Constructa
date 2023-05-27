﻿using System.Collections.Generic;
using GenesisConstructa.Structures;
using GenesisConstructa.UnlockingSystem;
using GenesisConstructa.Utility.Validating;
using UnityEngine;

namespace GenesisConstructa.BuildSystem
{
    [CreateAssetMenu(fileName = "Build_Data", menuName = "Build Data")]
    public class BuildDataSO : ScriptableObject, IUnlockableContainer
    {
        [SerializeField] private BuildData data;
        [SerializeField] private StructureType structureType;

        public BuildData Data => data.GetDeepCopy();
        public IEnumerable<IUnlockable> Unlockables => new[] {data};


        private void OnValidate()
        {
            SetIdentifierForBuildData();
            
            Validator.Validate(this);
        }


        private void SetIdentifierForBuildData()
        {
            data.Identifier = new StructureIdentifier(structureType, 0);
        }
        
        
#if !UNITY_EDITOR
        private void OnEnable()
        {
            SetIdentifierForBuildData();
        }
#endif
    }
}