using UnityEditor;
using UnityEngine;

namespace MoonPioneerClone.Utility
{
    public static class Logger
    {
        private const string TOGGLE_LOGGING_MENU_NAME = "Tools/Logger/Logging";
        
        private static bool _logging = true;
        

        [MenuItem(TOGGLE_LOGGING_MENU_NAME)]
        public static void ToggleLogging()
        {
            _logging = !_logging;

            MenuItemToggler.Toggle(TOGGLE_LOGGING_MENU_NAME, _logging);
        }


        public static void Log(object message)
        {
            if (!_logging)
            {
                return;
            }
            
            Debug.Log(message);
        }


        public static void Log(object sender, object message)
        {
            Log($"[{sender}]: {message}");
        }
        
        
        /// <summary>
        /// Logs string of 6 identical random letters
        /// </summary>
        public static void LogRandom()
        {
            string message = GetRandomMessage();

            Log(message);
        }
        
        
        /// <summary>
        /// Logs string of 6 identical random letters
        /// </summary>
        public static void LogRandom(object sender)
        {
            string message = GetRandomMessage();

            Log(sender, message);
        }


        private static string GetRandomMessage()
        {
            const int symbolsCount = 6;
            char symbol = (char) Random.Range(65, 91);
            symbol = Random.value >= 0.5f ? char.ToLower(symbol) : symbol;
                    
            return new string(symbol, symbolsCount);
        }
    }
}