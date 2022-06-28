using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ColonizationMobileGame.SceneLoading
{
    public class Bootstrapper : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private List<SceneAsset> scenesToLoad;
#endif
        [SerializeField, HideInInspector] private List<string> scenesNames = new List<string>();


#if UNITY_EDITOR
        private void OnValidate()
        {
            scenesNames = scenesToLoad.Select(sceneAsset => sceneAsset.name).ToList();
        }
#endif


        private void Awake()
        {
#if UNITY_EDITOR
            foreach (SceneAsset sceneAsset in scenesToLoad)
            {
                LoadScene(sceneAsset.name);
            }
#else
            foreach (string sceneName in scenesNames)
            {
                LoadScene(sceneName);
            }
#endif
        }


        private void LoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }
}