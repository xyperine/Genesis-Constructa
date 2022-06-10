namespace ColonizationMobileGame.ObjectPooling
{
    public interface IPoolable
    {
        void SetPool(ItemsPool pool);
        void Return();
    }
}