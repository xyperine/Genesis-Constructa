namespace GenesisConstructa
{
    public interface IChain<out T>
    {
        T Current { get; }
    }
}