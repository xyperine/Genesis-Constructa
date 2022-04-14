using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.UnlockingSystem
{
    [Serializable]
    public abstract class Unlockable
    {
        [TableColumnWidth(64, false)]
        [SerializeField] private bool locked;

        public bool Locked => locked;


        public virtual void Unlock()
        {
            locked = false;
        }
    }
}