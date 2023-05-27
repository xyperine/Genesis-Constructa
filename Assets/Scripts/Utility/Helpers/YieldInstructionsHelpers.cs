using System.Collections.Generic;
using UnityEngine;

namespace GenesisConstructa.Utility.Helpers
{
    public static class YieldInstructionsHelpers
    {
        private static readonly Dictionary<float, WaitForSeconds> WaitForSecondsDictionary = new Dictionary<float, WaitForSeconds>();
        private static readonly Dictionary<float, WaitForSecondsRealtime> WaitForSecondsRealtimeDictionary =
            new Dictionary<float, WaitForSecondsRealtime>();


        public static WaitForSeconds GetWaitForSeconds(float time)
        {
            if (WaitForSecondsDictionary.TryGetValue(time, out WaitForSeconds waitForSeconds))
            {
                return waitForSeconds;
            }
            
            WaitForSecondsDictionary[time] = new WaitForSeconds(time);
            return WaitForSecondsDictionary[time];
        }
        
        
        public static WaitForSecondsRealtime GetWaitForSecondsRealtime(float time)
        {
            if (WaitForSecondsRealtimeDictionary.TryGetValue(time, out WaitForSecondsRealtime waitForSecondsRealtime))
            {
                return waitForSecondsRealtime;
            }
            
            WaitForSecondsRealtimeDictionary[time] = new WaitForSecondsRealtime(time);
            return WaitForSecondsRealtimeDictionary[time];
        }
    }
}