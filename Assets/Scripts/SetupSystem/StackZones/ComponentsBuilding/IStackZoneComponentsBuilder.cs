using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;

namespace ColonizationMobileGame.SetupSystem.StackZones.ComponentsBuilding
{
    public interface IStackZoneComponentsBuilder
    {
        void Build(StackZoneSetupData data, StackZone zone);
    }
}