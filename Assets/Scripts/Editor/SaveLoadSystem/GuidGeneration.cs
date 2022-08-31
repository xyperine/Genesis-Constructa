using System.Linq;
using ColonizationMobileGame.SaveLoadSystem;
using UnityEditor;
using UnityEngine;

namespace ColonizationMobileGame.Editor.SaveLoadSystem
{
    internal static class GuidGeneration
    {
        [MenuItem("Saving/Generate Guids")]
        private static void GenerateGuids()
        {
            foreach (MonoBehaviour monoBehaviour in Object.FindObjectsOfType<MonoBehaviour>(true).Where(m => m is ISaveableWithGuid))
            {
                ISaveableWithGuid saveable = (ISaveableWithGuid) monoBehaviour;
                saveable.Guid.Set(PermanentGuid.NewGuid());
                EditorUtility.SetDirty(monoBehaviour);
            }
        }
    }
}