using GenesisConstructa.Utility.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace GenesisConstructa.GameFading
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