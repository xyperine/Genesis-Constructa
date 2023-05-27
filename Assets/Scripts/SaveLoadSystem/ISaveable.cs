namespace GenesisConstructa.SaveLoadSystem
{
    public interface ISaveable
    {
        object Save();
        void Load(object data);
    }
}