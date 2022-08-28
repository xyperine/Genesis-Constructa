namespace ColonizationMobileGame.SaveLoadSystem
{
    public interface ISaveableWithGuid : ISaveable
    {
        PermanentGuid Guid { get; }
    }
}