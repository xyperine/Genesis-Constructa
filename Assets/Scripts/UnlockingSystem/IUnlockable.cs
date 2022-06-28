using System;

namespace ColonizationMobileGame.UnlockingSystem
{
    public interface IUnlockable
    {
        bool Locked { get; }
        UnlockIdentifier Identifier { get; }
        
        event Action Unlocked;

        void Unlock();
    }
}