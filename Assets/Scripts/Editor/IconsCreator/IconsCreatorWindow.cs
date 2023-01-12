using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace IconsCreatorNS
{
    public class IconsCreatorWindow : EditorWindow
    {
        [SerializeField] private new string name = "Icon";
        [SerializeField] private int resolution = 512;

        [SerializeField] private GameObject targetObject;

        [SerializeField] private TextureImporterCompression compression = TextureImporterCompression.Compressed;
        [SerializeField] private FilterMode filterMode = FilterMode.Bilinear;

        private bool _advancedUnfolded;
        
        private static readonly IconsCreatorCameraUtility CameraUtility = new IconsCreatorCameraUtility();
        private readonly IconsCreator _iconsCreator = new IconsCreator(CameraUtility);

        private Texture2D _previewTexture;

        #region --- Window name ---

        private const string MENU_NAME = "Tools/Icons Creator";
        private const string HOTKEYS = "%#I";
        private const string FULL_MENU_NAME = MENU_NAME + " " + HOTKEYS;
        
        private const string TITLE = "Icons Creator";

        #endregion
        
        #region --- Serialized properties ---
        
        private SerializedObject _serializedObject;

        private SerializedProperty _nameSerializedProperty;
        private SerializedProperty _resolutionSerializedProperty;
        private SerializedProperty _targetObjectSerializedProperty;
        private SerializedProperty _compressionSerializedProperty;
        private SerializedProperty _filterModeSerializedProperty;
        
        #endregion
        

        [DrawGizmo(GizmoType.Active)]
        private static void DrawDebugGizmos(Transform component, GizmoType gizmoType)
        {
            Color centerColor = new Color(0.93f, 0.19f, 0.51f);
            Color minColor = new Color(0.04f, 0.35f, 0.77f);
            Color maxColor = new Color(1f, 0.42f, 0.18f);
            
            Handles.color = centerColor;
            Vector3 normal = -SceneView.currentDrawingSceneView.camera.transform.forward;
            Handles.DrawSolidDisc(CameraUtility.TargetBoundsCenter, normal, 0.25f);
            
            Handles.DrawLine(CameraUtility.CameraTransform.position, CameraUtility.TargetBoundsCenter);

            Bounds targetBounds = CameraUtility.TargetBounds;
            Handles.color = minColor;
            Handles.DrawSolidDisc(targetBounds.min, normal, 0.2f);
            Handles.color = maxColor;
            Handles.DrawSolidDisc(targetBounds.max, normal, 0.2f);
            Handles.color = Color.white;
            Handles.DrawWireCube(targetBounds.center, targetBounds.size);
        }


        [MenuItem(FULL_MENU_NAME)]
        private static void OpenWindow()
        {
            GetWindow<IconsCreatorWindow>(TITLE);
            
            AddIconsCreationCameraTag();
        }


        private static void AddIconsCreationCameraTag()
        {
            if (!InternalEditorUtility.tags.Contains(CameraUtility.IconsCreationCameraTag))
            {
                InternalEditorUtility.AddTag(CameraUtility.IconsCreationCameraTag);
            }
        }


        private void OnEnable()
        {
            Load();
            
            SetupSerializedProperties();
            
            _iconsCreator.SetData(name, compression, filterMode);
            CameraUtility.SetData(targetObject, resolution);
            
            CameraUtility.RetrieveCamera();
        }


        private void Load()
        {
            name = EditorPrefs.GetString(nameof(name));
            resolution = EditorPrefs.GetInt(nameof(resolution));
            compression = (TextureImporterCompression) EditorPrefs.GetInt(nameof(compression));
            filterMode = (FilterMode) EditorPrefs.GetInt(nameof(filterMode));
        }


        private void SetupSerializedProperties()
        {
            _serializedObject = new SerializedObject(this);
            
            _nameSerializedProperty = _serializedObject.FindProperty(nameof(name));
            _resolutionSerializedProperty = _serializedObject.FindProperty(nameof(resolution));
            _targetObjectSerializedProperty = _serializedObject.FindProperty(nameof(targetObject));
            _compressionSerializedProperty = _serializedObject.FindProperty(nameof(compression));
            _filterModeSerializedProperty = _serializedObject.FindProperty(nameof(filterMode));
        }


        private void OnDisable()
        {
            Save();
        }


        private void Save()
        {
            EditorPrefs.SetString(nameof(name), name);
            EditorPrefs.SetInt(nameof(resolution), resolution);
            EditorPrefs.SetInt(nameof(compression), (int) compression);
            EditorPrefs.SetInt(nameof(filterMode), (int) filterMode);
        }


        private void OnValidate()
        {
            if (!targetObject)
            {
                return;
            }
            
            _iconsCreator.SetData(name, compression, filterMode);
            CameraUtility.SetData(targetObject, resolution);

            CameraUtility.RetrieveCamera();
            CameraUtility.AdjustCamera();
        }


        protected void OnGUI()
        {
            _serializedObject.Update();
            
            DrawSettings();
            
            GUILayout.Space(8f);
            
            DrawButtons();
            
            GUILayout.Space(8f);

            if (_serializedObject.ApplyModifiedProperties())
            {
                _previewTexture = CameraUtility.CaptureCameraView();
            }
            
            DrawPreview();
        }


        private void DrawSettings()
        {
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("Settings", EditorStyles.boldLabel);
                GUILayout.Space(4f);

                DrawBasic();
            
                GUILayout.Space(8f);

                DrawAdvanced();
            }
        }
        

        private void DrawBasic()
        {
            EditorGUILayout.PropertyField(_nameSerializedProperty);
            EditorGUILayout.IntSlider(_resolutionSerializedProperty, 1, 1024);
            EditorGUILayout.PropertyField(_targetObjectSerializedProperty);
        }


        private void DrawAdvanced()
        {
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                _advancedUnfolded = EditorGUILayout.Foldout(_advancedUnfolded, "Advanced");

                if (!_advancedUnfolded)
                {
                    return;
                }

                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUILayout.PropertyField(_compressionSerializedProperty);
                    EditorGUILayout.PropertyField(_filterModeSerializedProperty);
                }
            }
        }


        private void DrawButtons()
        {
            using (new EditorGUI.DisabledScope(!targetObject))
            {
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    GUILayout.Label("Actions", EditorStyles.boldLabel);
                    GUILayout.Space(4f);

                    using (new GUILayout.HorizontalScope())
                    {
                        if (GUILayout.Button("Adjust Camera"))
                        {
                            CameraUtility.AdjustCamera();
            
                            _previewTexture = CameraUtility.CaptureCameraView();
                        }

                        if (GUILayout.Button("Create Icon"))
                        {
                            CameraUtility.AdjustCamera();
                    
                            _previewTexture = CameraUtility.CaptureCameraView();
                            _iconsCreator.CreateIcon();
                        }
                    }
                }
            }
        }


        private void DrawPreview()
        {
            if (!targetObject)
            {
                return;
            }
            
            if (!_previewTexture)
            {
                return;
            }

            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("Preview", EditorStyles.boldLabel);
                GUILayout.Space(4f);
                GUILayout.Box(_previewTexture);
            }
        }
    }
}