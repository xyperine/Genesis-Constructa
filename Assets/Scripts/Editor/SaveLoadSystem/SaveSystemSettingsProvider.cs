using ColonizationMobileGame.SaveLoadSystem;
using Hextant.Editor;
using UnityEditor;

namespace ColonizationMobileGame.Editor.SaveLoadSystem
{
    public static class SaveSystemSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider GetSettingsProvider()
        {
            return SaveSystemSettings.instance.GetSettingsProvider();
        }
    }
}