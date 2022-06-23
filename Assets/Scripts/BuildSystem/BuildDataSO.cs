using System.Collections.Generic;
using ColonizationMobileGame.UnlockingSystem;
using UnityEngine;

namespace ColonizationMobileGame.BuildSystem
{
    [CreateAssetMenu(fileName = "Build_Data", menuName = "Build Data")]
    public class BuildDataSO : ScriptableObject, IUnlockableContainer
    {
        [SerializeField] private BuildData data;
        [SerializeField] private StructureType structureType;

        public BuildData Data => data;
        
        public IEnumerable<IUnlockable> Unlockables => new[] {data};


        private void OnValidate()
        {
            SetIdentifierForBuildData();
        }


        private void SetIdentifierForBuildData()
        {
            data.Identifier = new UnlockIdentifier(structureType, 0);
        }
        
        
#if !UNITY_EDITOR
        private void OnEnable()
        {
            SetCoordsForBuildData();
        }
#endif
    }
}