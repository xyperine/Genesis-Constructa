using System.Collections.Generic;
using ColonizationMobileGame.ItemsRequirementsSystem;
using ColonizationMobileGame.UnlockingSystem;
using ColonizationMobileGame.Utility.Validating;
using UnityEngine;

namespace ColonizationMobileGame.BuildSystem
{
    [CreateAssetMenu(fileName = "Build_Data", menuName = "Build Data")]
    public class BuildDataSO : ScriptableObject, IUnlockableContainer, IChain<BuildData>
    {
        [SerializeField] private BuildData data;
        [SerializeField] private StructureType structureType;

        private readonly Validator _validator = new Validator();
        
        public BuildData Current => data;
        
        public IEnumerable<IUnlockable> Unlockables => new[] {data};
        public ItemsRequirementsChain RequirementsChain => new ItemsRequirementsChain(new[] {data.Price});


        private void OnValidate()
        {
            SetIdentifierForBuildData();
            _validator.Validate(this);
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