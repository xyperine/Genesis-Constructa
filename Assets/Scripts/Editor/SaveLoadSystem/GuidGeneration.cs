﻿using System.Linq;
using GenesisConstructa.SaveLoadSystem;
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
                identifiable.Guid.TrySet(PermanentGuid.NewGuid());
                EditorUtility.SetDirty(monoBehaviour);
            }
        }
    }
}