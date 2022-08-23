namespace ColonizationMobileGame.SaveLoadSystem
{
    public interface ISaveable
    {
        string Guid { get; }

        object Save();
        void Load(object data);
    }
}