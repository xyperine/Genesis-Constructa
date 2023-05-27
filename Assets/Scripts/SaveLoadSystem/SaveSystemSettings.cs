using Hextant;
using UnityEngine;

namespace GenesisConstructa.SaveLoadSystem
{
    [Settings(SettingsUsage.RuntimeProject, "Save System")]
    public sealed class SaveSystemSettings : Settings<SaveSystemSettings>
    {
        [SerializeField] private string fileName = "save";
        [SerializeField] private string fileExtension = ".json";

        public string FileName => fileName;
        public string FileExtension => fileExtension;

        public string FullPath => string.Join('/', Application.persistentDataPath, fileName + fileExtension);
    }
}