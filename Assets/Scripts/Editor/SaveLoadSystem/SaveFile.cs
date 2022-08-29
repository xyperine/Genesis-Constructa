using System.Diagnostics;
using System.IO;
using ColonizationMobileGame.SaveLoadSystem;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace ColonizationMobileGame.Editor.SaveLoadSystem
{
    internal static class SaveFile
    {
        private static string FullPath => SaveSystemSettings.instance.FullPath;
        
        
        [MenuItem("Saving/Save File/Open")]
        private static void Open()
        {
            if (!File.Exists(FullPath))
            {
                Debug.LogWarning($"{FullPath} was not found!");
                return;
            }
            
            Process.Start(FullPath);
        }
        
        
        [MenuItem("Saving/Save File/Clear")]
        private static void Clear()
        {
            File.Delete(FullPath);
        }
    }
}