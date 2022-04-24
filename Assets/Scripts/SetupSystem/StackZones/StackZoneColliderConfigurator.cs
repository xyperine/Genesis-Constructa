using UnityEditor;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.StackZones
{
    public class StackZoneColliderConfigurator
    {
        private GameObject _objForCollider;


        public void ConfigureCollider()
        {
            if (!_objForCollider)
            {
                Debug.LogWarning("There is no Collider gameobject");
                return;
            }
            
            Selection.activeGameObject = _objForCollider;
            SceneView.FrameLastActiveSceneView();
        }
    }
}