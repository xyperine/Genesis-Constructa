using ColonizationMobileGame.SaveLoadSystem;
using UnityEditor;
using UnityEngine;

namespace ColonizationMobileGame.Editor.SaveLoadSystem
{
    internal static class SaveLoadManagerSelector
    {
        [MenuItem("Saving/Select Manager")]
        private static void SelectManager()
        {
            SaveLoadManager saveLoadManager = Object.FindObjectOfType<SaveLoadManager>();

            if (!saveLoadManager)
            {
                Debug.LogWarning("Save Load Manager wasn't found!");
                return;
            }
            
            EditorGUIUtility.PingObject(saveLoadManager);
            Selection.SetActiveObjectWithContext(saveLoadManager, null);
        }
    }
}