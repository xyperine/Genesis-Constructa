using System;
using System.Collections.Generic;
using System.Linq;
using MoonPioneerClone.Utility.Observing;

namespace MoonPioneerClone.UpgradesSystem.Upgrading
{
    public class UpgradesStatusTracker<TUpgradeData>
        where TUpgradeData : UpgradeData
    {
        private readonly ObservingCollection<Upgrade<TUpgradeData>> _upgrades = new ObservingCollection<Upgrade<TUpgradeData>>();

        private Upgrade<TUpgradeData> _lastUnlockedUpgrade;
        private TUpgradeData _lastPurchasedUpgrade;

        public event Action<TUpgradeData> Purchased;
        public event Action<Upgrade<TUpgradeData>> Unlocked;


        public UpgradesStatusTracker(IEnumerable<Upgrade<TUpgradeData>> upgrades)
        {
            foreach (Upgrade<TUpgradeData> upgrade in upgrades)
            {
                _upgrades.Add(upgrade);
            }

            _upgrades.Changed += CheckForChanges;
        }


        private void CheckForChanges()
        {
            if (_lastUnlockedUpgrade != GetLastUnlockedUpgrade())
            {
                _lastUnlockedUpgrade = GetLastUnlockedUpgrade();
                Unlocked?.Invoke(_lastUnlockedUpgrade);
            }
            
            if (_lastPurchasedUpgrade != GetLastPurchasedUpgrade())
            {
                _lastPurchasedUpgrade = GetLastPurchasedUpgrade();
                Purchased?.Invoke(_lastPurchasedUpgrade);
            }
        }


        private Upgrade<TUpgradeData> GetLastUnlockedUpgrade()
        {
            return _upgrades.LastOrDefault(u => !u.Locked);
        }


        private TUpgradeData GetLastPurchasedUpgrade()
        {
            return _upgrades.LastOrDefault(u => u.Satisfied)?.Data;
        }
    }
}