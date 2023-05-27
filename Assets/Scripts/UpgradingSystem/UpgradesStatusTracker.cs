using System;
using System.Collections.Generic;

namespace GenesisConstructa.UpgradingSystem
{
    public sealed class UpgradesStatusTracker<TUpgradeData>
        where TUpgradeData : UpgradeData
    {
        public event Action<TUpgradeData> Purchased;


        public UpgradesStatusTracker(IEnumerable<Upgrade<TUpgradeData>> upgrades)
        {
            foreach (Upgrade<TUpgradeData> upgrade in upgrades)
            {
                upgrade.Purchased += OnUpgradePurchased;
            }
        }


        private void OnUpgradePurchased(Upgrade<TUpgradeData> upgrade)
        {
            upgrade.Purchased -= OnUpgradePurchased;

            Purchased?.Invoke(upgrade.Data);
        }
    }
}