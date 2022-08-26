namespace ColonizationMobileGame.SaveLoadSystem
{
    public interface ISaveable
    {
        PermanentGuid Guid { get; }
        
        object Save();
        void Load(object data);
    }
}