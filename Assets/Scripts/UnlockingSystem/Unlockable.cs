using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.UnlockingSystem
{
    [Serializable]
    public abstract class Unlockable
    {
        [TableColumnWidth(64, false)]
        [SerializeField] private bool locked;

        public bool Locked => locked;

        public event Action<Unlockable> Unlocked;


        public virtual void Unlock()
        {
            locked = false;
            
            Unlocked?.Invoke(this);
        }
    }
}