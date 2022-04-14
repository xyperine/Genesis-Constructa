using System;
using UnityEngine;

namespace MoonPioneerClone.UpgradesSystem.Unlocking
{
    [Serializable]
    public abstract class Unlockable
    {
        [SerializeField] private bool locked;

        public bool Locked => locked;


        public virtual void Unlock()
        {
            locked = false;
        }
    }
}