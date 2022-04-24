using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;

namespace MoonPioneerClone.SetupSystem.StackZones.ComponentsBuilding
{
    public interface IStackZoneComponentsBuilder
    {
        void Setup(StackZoneSetupData data, StackZone zone);
    }
}