namespace ColonizationMobileGame.UpgradingSystem
{
    public interface IUpgradeable<in TUpgradeData>
        where TUpgradeData : UpgradeData
    {
        public void Upgrade(TUpgradeData data);
    }
}