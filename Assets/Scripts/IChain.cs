namespace ColonizationMobileGame
{
    public interface IChain<out T>
    {
        T Current { get; }
    }
}