using GenesisConstructa.SaveLoadSystem;
using Hextant.Editor;
using UnityEditor;

namespace ColonizationMobileGame.Editor.SaveLoadSystem
{
    internal static class SaveSystemSettingsProvider
    {
        [SettingsProvider]
        private static SettingsProvider GetSettingsProvider()
        {
            return SaveSystemSettings.instance.GetSettingsProvider();
        }
    }
}