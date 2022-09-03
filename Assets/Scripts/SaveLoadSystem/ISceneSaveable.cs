namespace ColonizationMobileGame.SaveLoadSystem
{
    public interface ISceneSaveable : ISaveable, IPermanentGuidIdentifiable
    {
        int LoadingOrder { get; }
    }
}