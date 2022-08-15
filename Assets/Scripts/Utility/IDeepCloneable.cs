namespace ColonizationMobileGame.Utility
{
    public interface IDeepCloneable<out T>
        where T : IDeepCloneable<T>
    {
        T GetDeepCopy();
    }
}