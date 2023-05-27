using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GenesisConstructa.UI.ArrowPointers
{
    public class ArrowPointersDrawer : MonoBehaviour
    {
        [SerializeField] private Camera canvasCamera;
        [SerializeField] private ArrowPointer arrowPointerPrefab;

        private readonly List<ArrowPointer> _arrowPointers = new List<ArrowPointer>();

        
        private void Awake()
        {
            InitializePointers();
        }


        private void InitializePointers()
        {
            for (int i = 0; i < 10; i++)
            {
                _arrowPointers.Add(Instantiate(arrowPointerPrefab, transform));
                _arrowPointers[i].SetCamera(canvasCamera);
                _arrowPointers[i].Disable();
            }
        }


        public void Draw(ArrowPointerTarget target)
        {
            if (_arrowPointers.Exists(p => p.IsAlreadyPointingTo(target)))
            {
                return;
            }
            
            _arrowPointers.FirstOrDefault(p => p.Free)?.PointTo(target);
        }


        public void StopDrawing(ArrowPointerTarget target)
        {
            ArrowPointer pointer = _arrowPointers.FirstOrDefault(p => p.IsAlreadyPointingTo(target));
            if (pointer)
            {
                pointer.Disable();
            }
        }
    }
}