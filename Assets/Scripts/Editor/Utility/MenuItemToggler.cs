using UnityEditor;

namespace ColonizationMobileGame.Editor.Utility
{
    internal static class MenuItemToggler
    {
        public static void Toggle(string path, bool value)
        {
            Menu.SetChecked(path, value);
            EditorPrefs.SetBool(path, value);
        }
    }
}