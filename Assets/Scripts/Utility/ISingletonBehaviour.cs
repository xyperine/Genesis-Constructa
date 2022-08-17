namespace ColonizationMobileGame.Utility
{
    public interface ISingletonBehaviour<in T>
    {
        void SetInstance(T instance);
    }
}