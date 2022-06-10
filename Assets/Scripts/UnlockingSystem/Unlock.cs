using System;
using ColonizationMobileGame.ItemsRequirementsSystem;
using ColonizationMobileGame.Utility.Validating;
using UnityEngine;

namespace ColonizationMobileGame.UnlockingSystem
{
    [Serializable]
    public class Unlock : IValidatable
    {
        [SerializeField] private ItemsRequirementsBlock price;
        private Unlockable _unlockable;


        public Unlock(Unlockable unlockable)
        {
            _unlockable = unlockable;
        }


        public void OnValidate()
        {
            price.Satisfied += _unlockable.Unlock;
        }
    }
}