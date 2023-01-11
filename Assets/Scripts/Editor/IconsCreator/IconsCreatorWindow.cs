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
        
        #region --- Gizmos ---
        
        private static Vector3 _t;
        private static Vector3 _c;
        private static Vector3 _d;
        private static Vector3 _s;
        private static Bounds _targetBounds;
        private static Transform _camT;


        //[DrawGizmo(GizmoType.Active)]
        private static void DrawGizmos(Transform component, GizmoType gizmoType)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_t, 0.5f);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_camT.position, _camT.forward * 100);
            
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_c, 0.5f);
            
            Gizmos.DrawRay(_camT.position, _d);
            
            Handles.DrawWireCube(_targetBounds.center, _targetBounds.size);
            Gizmos.DrawSphere(_targetBounds.min, 0.2f);
            Gizmos.DrawSphere(_targetBounds.max, 0.2f);
        }
        
        #endregion


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
            CameraUtility.PositionCamera();
            CameraUtility.SetCameraSize();
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
                            CameraUtility.CenterCamera();
                            CameraUtility.SetCameraSize();
            
                            _previewTexture = CameraUtility.CaptureCameraView();
                        }

                        if (GUILayout.Button("Create Icon"))
                        {
                            CameraUtility.CenterCamera();
                            CameraUtility.SetCameraSize();
                    
                            _iconsCreator.CreateIcon();
                            _previewTexture = CameraUtility.CaptureCameraView();
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