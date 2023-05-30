using System.Linq;
using UnityEngine;

namespace GenesisConstructa.Conveyor
{
    public class ConveyorBeltRenderer : MonoBehaviour
    {
        [SerializeField] private new Renderer renderer;
        [SerializeField] private Material material;
        
        private static readonly int SpeedShaderPropertyID = Shader.PropertyToID("_Speed");

        private Material _runtimeMaterial;


        private void Awake()
        {
            _runtimeMaterial = renderer.materials.Single(m => m.name[..material.name.Length] == material.name);
        }


        public void UpdateMaterial(float speed)
        {
            Material material = Application.isPlaying ? 
                _runtimeMaterial : this.material;
            
            SetMaterialSpeed(material, speed);
        }


        private void SetMaterialSpeed(Material material, float speed)
        {
            const float speedSyncMultiplier = 0.182f;
            material?.SetFloat(SpeedShaderPropertyID, speed * speedSyncMultiplier);
        }
    }
}