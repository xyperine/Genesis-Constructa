using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace SimpleIconsCreatorNS
{
    public class SimpleIconsCreator : MonoBehaviour
    {
        [BoxGroup("Camera")]
        [SerializeField] private new Camera camera;
        [BoxGroup("Camera")]
        [SerializeField, Range(0.1f, 10f)] private float zoom = 1f;
        [BoxGroup("Image")]
        [SerializeField] private new string name;
        [BoxGroup("Image")]
        [SerializeField] private int resolution = 512;

        [FoldoutGroup("Advanced")]
        [SerializeField] private TextureImporterCompression compression = TextureImporterCompression.Compressed;
        [FoldoutGroup("Advanced")]
        [SerializeField] private FilterMode filterMode = FilterMode.Bilinear;

        private const string DIRECTORY = "/Textures/Icons/";
        
        private float _cameraDefaultOrthographicSize;


        [PropertySpace]
        [Button(ButtonSizes.Medium)]
        public void CreateIcon()
        {
            if (!camera.orthographic)
            {
                throw new InvalidDataException("Camera has to be in orthographic mode!");
            }

            SetupCamera();

            Texture2D icon = CaptureCameraView();

            SaveIcon(icon);
        }


        private void SetupCamera()
        {
            _cameraDefaultOrthographicSize = camera.orthographicSize;
            camera.orthographicSize = _cameraDefaultOrthographicSize / zoom;

            camera.targetTexture = RenderTexture.GetTemporary(resolution, resolution);
        }


        private Texture2D CaptureCameraView()
        {
            RenderTexture.active = camera.targetTexture;

            camera.Render();
            camera.orthographicSize = _cameraDefaultOrthographicSize;

            Texture2D image = new Texture2D(resolution, resolution);
            image.ReadPixels(new Rect(0, 0, resolution, resolution), 0, 0);
            image.Apply();

            camera.targetTexture = null;
            RenderTexture.active = null;

            return image;
        }


        private void SaveIcon(Texture2D image)
        {
            byte[] bytes = image.EncodeToPNG();
            DestroyImmediate(image);

            string path = Application.dataPath + DIRECTORY + name + ".png";

            File.WriteAllBytes(path, bytes);
            AssetDatabase.Refresh();

            ConvertToSprite(path);
        }


        private void ConvertToSprite(string path)
        {
            string relativePath = path.Remove(0, Application.dataPath.Length - "Assets".Length);

            TextureImporter textureImporter = (TextureImporter) AssetImporter.GetAtPath(relativePath);
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.textureCompression = compression;
            textureImporter.filterMode = filterMode;
            textureImporter.mipmapEnabled = false;

            textureImporter.SaveAndReimport();
            AssetDatabase.Refresh();

            EditorGUIUtility.PingObject(textureImporter);
        }
    }
}