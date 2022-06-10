using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsRequirementsSystem;

namespace ColonizationMobileGame.UpgradingSystem
{
    public class UpgradesChain<TUpgradeData>
        where TUpgradeData : UpgradeData
    {
        public UpgradesStatusTracker<TUpgradeData> UpgradesStatusTracker { get; }
        public ItemsRequirementsChain RequirementsChain { get; }


        public UpgradesChain(IEnumerable<Upgrade<TUpgradeData>> upgrades)
        {
            Upgrade<TUpgradeData>[] copies = upgrades.Select(u => u.GetDeepCopy()).ToArray();
            
            UpgradesStatusTracker = new UpgradesStatusTracker<TUpgradeData>(copies);
            RequirementsChain = new ItemsRequirementsChain(copies.Select(u => u.Price).ToArray());
        }
    }
}