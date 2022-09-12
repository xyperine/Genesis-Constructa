using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers
{
    public class ArrowPointersManager : MonoBehaviour
    {
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
                _arrowPointers[i].gameObject.SetActive(false);
                _arrowPointers[i].SetCamera(Camera.main);
            }
        }


        public void PointTo(ArrowPointerTarget target)
        {
            if (_arrowPointers.Exists(p => p.Target == target))
            {
                return;
            }

            _arrowPointers.FirstOrDefault(p => p.Free)?.PointTo(target);
        }
    }
}