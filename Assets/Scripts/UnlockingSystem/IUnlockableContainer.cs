using System.Collections.Generic;

namespace GenesisConstructa.UnlockingSystem
{
    public interface IUnlockableContainer
    {
        IEnumerable<IUnlockable> Unlockables { get; }
    }
}