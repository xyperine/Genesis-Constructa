using ColonizationMobileGame.Editor.Utility;
using ColonizationMobileGame.SaveLoadSystem;
using UnityEditor;
using UnityEngine;

namespace ColonizationMobileGame.Editor.SaveLoadSystem
{
    public static class SavingActivator
    {
        private const string MENU_NAME = "Saving/Active";

        private static bool _active = true;


        [MenuItem(MENU_NAME)]
        internal static void ToggleActive()
        {
            _active = !_active;
            
            SaveLoadManager saveLoadManager = Object.FindObjectOfType<SaveLoadManager>();
            saveLoadManager.SetActive(_active);
            
            EditorUtility.SetDirty(saveLoadManager);
            
            MenuItemToggler.Toggle(MENU_NAME, _active);
        }
    }
}