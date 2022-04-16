using System.Collections.Generic;
using UnityEngine;

namespace MoonPioneerClone.Utility
{
    public static class Helpers
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
        
        
        public static string GetGameObjectPath(Transform transform)
        {
            string path = transform.name;
            while (transform.parent != null)
            {
                transform = transform.parent;
                path = transform.name + "/" + path;
            }
            return path;
        }
        
        
        public static string GetGameObjectPathWithoutRoot(Transform transform)
        {
            string path = transform.name;
            while (transform.parent != null)
            {
                transform = transform.parent;
                if (transform == transform.root)
                {
                    continue;
                }
                path = transform.name + "/" + path;
            }
            return path;
        }
    }
}