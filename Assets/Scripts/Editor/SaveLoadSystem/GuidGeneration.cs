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
            foreach (MonoBehaviour monoBehaviour in Object.FindObjectsOfType<MonoBehaviour>(true).Where(m => m is IPermanentGuidIdentifiable))
            {
                IPermanentGuidIdentifiable identifiable = (IPermanentGuidIdentifiable) monoBehaviour;
                identifiable.Guid.Set(PermanentGuid.NewGuid());
                EditorUtility.SetDirty(monoBehaviour);
            }
        }
    }
}