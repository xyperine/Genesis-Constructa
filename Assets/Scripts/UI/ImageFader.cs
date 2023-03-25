using ColonizationMobileGame.Utility.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace ColonizationMobileGame.UI
{
    public class ImageFader : MonoBehaviour
    {
        [SerializeField] private Image image;


        private void LateUpdate()
        {
            image.color = image.color.WithAlpha(1f - Time.timeScale);
        }
    }
}