namespace ColonizationMobileGame.SaveLoadSystem
{
    public interface ISaveableWithGuid : ISaveable
    {
        SaveableType SaveableType { get; }
        PermanentGuid Guid { get; }
    }
}