using ColonizationMobileGame.Structures;
using UnityEngine;

namespace ColonizationMobileGame.DebugTools
{
    public class DebugOperationsWindow : MonoBehaviour
    {
        [SerializeField] private DebugOperations debugOperations;

        private StructureType _targetStructure;


        public void SetTarget(int targetInt)
        {
            StructureType targetStructure = (StructureType) targetInt;
            _targetStructure = targetStructure;
        }


        public void Unlock()
        {
            debugOperations.Unlock(_targetStructure);
        }


        public void UnlockAndBuild()
        {
            debugOperations.UnlockAndBuild(_targetStructure);
        }


        public void Upgrade()
        {
            debugOperations.Upgrade(_targetStructure);
        }
    }
}