namespace ColonizationMobileGame.Utility
{
    public interface IDeepCloneable<out T>
        where T : IDeepCloneable<T>
    {
        public T GetDeepCopy();
    }
}