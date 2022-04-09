using MoonPioneerClone.Utility;
using UnityEditor;

namespace MoonPioneerClone.Editor
{
    public static class LoggerToggler
    {
        private const string TOGGLE_LOGGING_MENU_NAME = "Tools/Logger/Logging";
        
        private static bool _logging = true;
        
        
        [MenuItem(TOGGLE_LOGGING_MENU_NAME)]
        public static void ToggleLogging()
        {
            _logging = !_logging;
            Logger.Toggle(_logging);
            
            MenuItemToggler.Toggle(TOGGLE_LOGGING_MENU_NAME, _logging);
        }
    }
}