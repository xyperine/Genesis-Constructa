using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;

namespace GenesisConstructa.SetupSystem.StackZones.ComponentsBuilding
{
    public interface IStackZoneComponentsBuilder
    {
        void Build(StackZoneSetupData data, StackZone zone);
    }
}