﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.TasksSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ColonizationMobileGame.Initialization
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private DependenciesResolver dependenciesResolver;
        [SerializeField] private TasksInitializer tasksInitializer;
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
                StartCoroutine(LoadScene(sceneName));
            }
#endif
        }


        private IEnumerator LoadScene(string sceneName)
        {
            AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            yield return new WaitUntil(() => loading.isDone);

            GameObject[] rootGameObjects = SceneManager.GetSceneByName(sceneName).GetRootGameObjects();
            
            dependenciesResolver.Resolve(rootGameObjects);
            tasksInitializer.InitializeTasks();
        }
    }
}