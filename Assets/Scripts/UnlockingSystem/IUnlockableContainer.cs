using System.Collections.Generic;

namespace ColonizationMobileGame.UnlockingSystem
{
    public interface IUnlockableContainer
    {
        IEnumerable<IUnlockable> Unlockables { get; }
    }
}