using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ColonizationMobileGame.UI
{
    public class UIDisabler : MonoBehaviour
    {
        [SerializeField] private Canvas[] ignoredCanvases;


        public void DisableUI()
        {
            GameObject[] allCanvasesGameObjects = FindObjectsOfType<Canvas>().Select(c => c.gameObject).ToArray();
            GameObject[] ignoredCanvasesGameObjects = ignoredCanvases.Select(c => c.gameObject).ToArray();

            foreach (GameObject canvasGameObject in allCanvasesGameObjects)
            {
                if (ignoredCanvasesGameObjects.Contains(canvasGameObject))
                {
                    continue;
                }
                
                canvasGameObject.SetActive(false);
            }
            
            EventSystem.current.enabled = false;
        }
    }
}