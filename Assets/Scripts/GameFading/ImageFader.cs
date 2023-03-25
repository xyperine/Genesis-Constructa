using ColonizationMobileGame.Utility.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace ColonizationMobileGame.GameFading
{
    public class ImageFader : MonoBehaviour
    {
        [SerializeField] private Image image;


        public void SetAlpha(float alpha)
        {
            image.color = image.color.WithAlpha(alpha);
        }
    }
}