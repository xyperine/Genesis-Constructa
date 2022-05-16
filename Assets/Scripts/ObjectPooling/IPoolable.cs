namespace MoonPioneerClone.ObjectPooling
{
    public interface IPoolable
    {
        void SetPool(ItemsPool pool);
        void Return();
    }
}