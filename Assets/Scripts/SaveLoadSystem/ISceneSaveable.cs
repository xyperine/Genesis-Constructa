namespace ColonizationMobileGame.SaveLoadSystem
{
    public interface ISceneSaveable : ISaveable, IPermanentGuidIdentifiable
    {
        SaveableType SaveableType { get; }
    }
}