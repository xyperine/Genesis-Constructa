using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;

namespace MoonPioneerClone.SetupSystem.StackZones.ComponentsBuilding
{
    public interface IStackZoneComponentsBuilder
    {
        void Build(StackZoneSetupData data, StackZone zone);
    }
}