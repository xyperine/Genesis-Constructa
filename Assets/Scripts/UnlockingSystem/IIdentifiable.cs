using ColonizationMobileGame.Structures;

namespace ColonizationMobileGame.UnlockingSystem
{
    public interface IIdentifiable
    {
        public StructureIdentifier Identifier { get; }
    }
}