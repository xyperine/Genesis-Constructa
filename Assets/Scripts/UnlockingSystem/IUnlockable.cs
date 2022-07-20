using System;

namespace ColonizationMobileGame.UnlockingSystem
{
    public interface IUnlockable : IIdentifiable
    {
        bool Locked { get; }
        
        event Action Unlocked;

        void Unlock();
    }
}