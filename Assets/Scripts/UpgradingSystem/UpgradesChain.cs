using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsRequirementsSystem;

namespace ColonizationMobileGame.UpgradingSystem
{
    public class UpgradesChain<TUpgradeData> : IPurchasableChain<Upgrade<TUpgradeData>>
        where TUpgradeData : UpgradeData
    {
        private readonly Upgrade<TUpgradeData>[] _upgrades;

        public UpgradesStatusTracker<TUpgradeData> UpgradesStatusTracker { get; }
        public ItemsRequirementsChain RequirementsChain { get; }
        public Upgrade<TUpgradeData> Current => _upgrades.FirstOrDefault(u => RequirementsChain.Current == u.Price);


        public UpgradesChain(IEnumerable<Upgrade<TUpgradeData>> upgrades)
        {
            Upgrade<TUpgradeData>[] copies = upgrades.Select(u => u.GetDeepCopy()).ToArray();

            _upgrades = copies;
            UpgradesStatusTracker = new UpgradesStatusTracker<TUpgradeData>(copies);
            RequirementsChain = new ItemsRequirementsChain(copies.Select(u => u.Price).ToArray());
        }
    }
}