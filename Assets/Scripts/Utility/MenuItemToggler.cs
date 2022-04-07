﻿using UnityEditor;

namespace MoonPioneerClone.Utility
{
    public static class MenuItemToggler
    {
        public static void Toggle(string path, bool value)
        {
            Menu.SetChecked(path, value);
            EditorPrefs.SetBool(path, value);
        }
    }
}