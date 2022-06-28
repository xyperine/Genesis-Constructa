using ColonizationMobileGame.ItemsExtraction;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsRequirementsSystem;
using ColonizationMobileGame.ObjectPooling;
using UnityEngine;

namespace ColonizationMobileGame.BuildSystem
{
    public class Builder : MonoBehaviour
    {
        [SerializeField] private BuildDataSO buildDataSO;
        [SerializeField] private Transform structuresParent;
        [SerializeField] private ItemsPool itemsPool;
        [SerializeField] private ItemsConsumer consumer;

        private Vector2 _bounds;
        private BuildData _buildData;

        
        private void Awake()
        {
            _buildData = buildDataSO.Data;
            
            SetupPrice();
            DrawBox();

            if (!_buildData.Locked)
            {
                return;
            }

            _buildData.Unlocked += OnUnlocked;
            gameObject.SetActive(false);
        }


        private void SetupPrice()
        {
            ItemsRequirementsBlock price = _buildData.Price.GetDeepCopy();
            
            consumer.Setup(new ItemsRequirementsChain(new []{price}));
            price.Satisfied += Build;
        }


        private void Build()
        {
            GameObject structure = Instantiate(_buildData.StructurePrefab, transform.position, Quaternion.identity, structuresParent);
            structure.GetComponentInChildren<ExtractorProductionUnit>().SetPool(itemsPool);
            
            Destroy(gameObject);
        }


        private void OnUnlocked()
        {
            gameObject.SetActive(true);
        }


        private void DrawBox()
        {
            //_bounds = buildData.Bounds;
            //Handles.DrawSolidRectangleWithOutline(new Rect(transform.position.x, transform.position.y, _bounds.x, _bounds.y), Color.blue, Color.cyan);
        }
    }
}