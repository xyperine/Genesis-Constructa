using System.Collections;
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
                StartCoroutine(LoadScene(sceneAsset.name));
            }
#else
            foreach (string sceneName in scenesNames)
            {
                LoadScene(sceneName);
            }
#endif
        }


        private IEnumerator LoadScene(string sceneName)
        {
            var s = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            yield return new WaitUntil(() => s.isDone);
            
            foreach (GameObject rootGameObject in SceneManager.GetSceneByName(sceneName).GetRootGameObjects())
            {
                foreach (Canvas canvas in rootGameObject.GetComponentsInChildren<Canvas>())
                {
                    canvas.worldCamera = Camera.main;
                }
            }
        }
    }
}