using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.GameFading;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.TutorialSystem;
using ColonizationMobileGame.Utility.Helpers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace ColonizationMobileGame.Initialization
{
    public class Bootstrapper : MonoBehaviour
#if UNITY_EDITOR
        , ISerializationCallbackReceiver
#endif
    {
        [SerializeField] private DependenciesResolver dependenciesResolver;
        [SerializeField] private SaveLoadManager saveLoadManager;
        [SerializeField] private TutorialBuilder tutorialBuilder;
        [SerializeField] private GameFader gameFader;
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
            DOTween.SetTweensCapacity(875, 50);

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
            gameFader.FadeOutImmediately(FadeFlags.Audio | FadeFlags.Visuals);
            
            AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            yield return new WaitUntil(() => loading.isDone);
            
            dependenciesResolver.ResolveBeforeRestoringSave();

            saveLoadManager.Initialize();
            tutorialBuilder.Initialize();
            
            dependenciesResolver.ResolveAfterRestoringSave();

            yield return YieldInstructionsHelpers.GetWaitForSecondsRealtime(2f);
            
            gameFader.BeginFadeIn(2f, FadeFlags.Audio | FadeFlags.Visuals);
        }


#if UNITY_EDITOR
        public void OnBeforeSerialize()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                UnloadScenes();
            }
        }


        private void UnloadScenes()
        {
            foreach (SceneAsset sceneAsset in scenesToLoad)
            {
                Scene scene = EditorSceneManager.GetSceneByName(sceneAsset.name);
                if (scene.isLoaded)
                {
                    EditorSceneManager.CloseScene(scene, false);
                }
            }
        }


        public void OnAfterDeserialize()
        {
            
        }
#endif
    }
}