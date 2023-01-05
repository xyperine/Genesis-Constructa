using System.Collections.Generic;
using UnityEngine;

namespace ColonizationMobileGame.Utility.Helpers
{
    public static class YieldInstructionsHelpers
    {
        private static readonly Dictionary<float, WaitForSeconds> WaitForSecondsDictionary = new Dictionary<float, WaitForSeconds>();


        public static WaitForSeconds GetWaitForSeconds(float time)
        {
            if (WaitForSecondsDictionary.TryGetValue(time, out WaitForSeconds waitForSeconds))
            {
                return waitForSeconds;
            }
            
            WaitForSecondsDictionary[time] = new WaitForSeconds(time);
            return WaitForSecondsDictionary[time];
        }
    }
}