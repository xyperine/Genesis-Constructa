using System;
using System.Linq;
using ColonizationMobileGame.SaveLoadSystem;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ColonizationMobileGame.Editor.SaveLoadSystem
{
    public static class GuidGeneration
    {
        [MenuItem("Saving/Generate Guids")]
        private static void GenerateGuids()
        {
            foreach (MonoBehaviour monoBehaviour in Object.FindObjectsOfType<MonoBehaviour>(true).Where(m => m is ISaveableWithGuid))
            {
                ISaveableWithGuid saveable = (ISaveableWithGuid) monoBehaviour;
                saveable.Guid.Set(Guid.NewGuid().ToString());
                EditorUtility.SetDirty(monoBehaviour);
            }
        }
    }
}