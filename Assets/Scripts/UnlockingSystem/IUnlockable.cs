using System;

namespace GenesisConstructa.UnlockingSystem
{
    public interface IUnlockable : IIdentifiable
    {
        bool Locked { get; }
        
        event Action Unlocked;

        void Unlock();
    }
}