using UnityEngine;

namespace MoonPioneerClone.Utility
{
    public static class Logger
    {
        private static bool _logging = true;


        public static void Toggle(bool logging)
        {
            _logging = logging;
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
        /// Logs string of 6 identical randomized letters
        /// </summary>
        public static void LogRandom()
        {
            string message = GetRandomMessage();

            Log(message);
        }
        
        
        /// <summary>
        /// Logs string of 6 identical randomized letters
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