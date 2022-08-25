using System.Diagnostics;
using System.IO;
using ColonizationMobileGame.SaveLoadSystem;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace ColonizationMobileGame.Editor.SaveLoadSystem
{
    public static class SaveFile
    {
        private static string FullPath => SaveSystemSettings.instance.FullPath;
        
        
        [MenuItem("Saving/Save File/Open")]
        public static void Open()
        {
            if (!File.Exists(FullPath))
            {
                Debug.LogWarning($"{FullPath} was not found!");
                return;
            }
            
            Process.Start(FullPath);
        }
        
        
        [MenuItem("Saving/Save File/Clear")]
        public static void Clear()
        {
            File.Delete(FullPath);
        }
    }
}