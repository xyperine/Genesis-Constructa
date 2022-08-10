using System.Linq;
using ColonizationMobileGame.UnlockingSystem;
using UnityEngine;

namespace ColonizationMobileGame.ProgressionMilestonesSystem
{
    public class MineralFactoryUnlockedMilestone : ProgressionMilestone
    {
        [SerializeField] private UnlocksChainSO unlocksChainSO;

        private Unlock _mineralFactoryUnlock;
        
        protected override ProgressionMilestoneType Type => ProgressionMilestoneType.MineralFactoryUnlocked;


        private void Awake()
        {
            GetMineralFactoryUnlock();
        }


        private void GetMineralFactoryUnlock()
        {
            _mineralFactoryUnlock = unlocksChainSO.Unlocks.SingleOrDefault(u =>
                u.Identifier.Equals(new StructureIdentifier(StructureType.MineralFactory, 0)));
        }
        

        protected override void Subscribe()
        {
            if (_mineralFactoryUnlock != null)
            {
                _mineralFactoryUnlock.Price.Fulfilled += InvokeAchieved;
            }
        }


        protected override void Unsubscribe()
        {
            if (_mineralFactoryUnlock != null)
            {
                _mineralFactoryUnlock.Price.Fulfilled -= InvokeAchieved;
            }
        }
    }
}