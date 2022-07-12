using ColonizationMobileGame.ItemsRequirementsSystem;

namespace ColonizationMobileGame
{
    public interface IChain<out T>
    {
        T Current { get; }
        ItemsRequirementsChain RequirementsChain { get; }
    }
}