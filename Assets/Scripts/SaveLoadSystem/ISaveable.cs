namespace ColonizationMobileGame.SaveLoadSystem
{
    public interface ISaveable
    {
        string Guid { get; }

        void SetGuid(string newGuid);
        object Save();
        void Load(object data);
    }
}