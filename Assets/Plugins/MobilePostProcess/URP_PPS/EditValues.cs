using UnityEngine;
using UnityEngine.Rendering.Universal;

[ExecuteInEditMode]
public class EditValues : MonoBehaviour
{
    public bool Blur = false;
    [Range(0, 1)]
    public float BlurAmount = 1f;
    public bool Bloom = false;
    public Color BloomColor = Color.white;
    [Range(0, 5)]
    public float BloomAmount = 1f;
    [Range(0, 1)]
    public float BloomDiffuse = 1f;
    [Range(0, 1)]
    public float BloomThreshold = 0f;
    [Range(0, 1)]
    public float BloomSoftness = 0f;

    public bool LUT = false;
    [Range(0, 1)]
    public float LutAmount = 0.0f;
    public Texture2D SourceLut = null;

    public bool ImageFiltering = false;
    public Color Color = Color.white;
    [Range(0, 1)]
    public float Contrast = 0f;
    [Range(-1, 1)]
    public float Brightness = 0f;
    [Range(-1, 1)]
    public float Saturation = 0f;
    [Range(-1, 1)]
    public float Exposure = 0f;
    [Range(-1, 1)]
    public float Gamma = 0f;
    [Range(0, 1)]
    public float Sharpness = 0f;

    public bool ChromaticAberration = false;
    public float Offset = 0;
    [Range(-1, 1)]
    public float FishEyeDistortion = 0;
    [Range(0, 1)]
    public float GlitchAmount = 0;

    public bool Distortion = false;
    [Range(0, 1)]
    public float LensDistortion = 0;

    public bool Vignette = false;
    public Color VignetteColor = Color.black;
    [Range(0, 1)]
    public float VignetteAmount = 0f;
    [Range(0.001f, 1)]
    public float VignetteSoftness = 0.001f;

    void Update()
    {
        if (PostProcessUrp.Instance == null) return;
        PostProcessUrp.Instance.settings.Blur = Blur;
        PostProcessUrp.Instance.settings.BlurAmount = BlurAmount;
        PostProcessUrp.Instance.settings.Bloom = Bloom;
        PostProcessUrp.Instance.settings.BloomColor = BloomColor;
        PostProcessUrp.Instance.settings.BloomAmount = BloomAmount;
        PostProcessUrp.Instance.settings.BloomDiffuse = BloomDiffuse;
        PostProcessUrp.Instance.settings.BloomThreshold = BloomThreshold;
        PostProcessUrp.Instance.settings.BloomSoftness = BloomSoftness;
        PostProcessUrp.Instance.settings.LUT = LUT;
        PostProcessUrp.Instance.settings.LutAmount = LutAmount;
        PostProcessUrp.Instance.settings.SourceLut = SourceLut;
        PostProcessUrp.Instance.settings.ImageFiltering = ImageFiltering;
        PostProcessUrp.Instance.settings.Color = Color;
        PostProcessUrp.Instance.settings.Contrast = Contrast;
        PostProcessUrp.Instance.settings.Brightness = Brightness;
        PostProcessUrp.Instance.settings.Saturation = Saturation;
        PostProcessUrp.Instance.settings.Exposure = Exposure;
        PostProcessUrp.Instance.settings.Gamma = Gamma;
        PostProcessUrp.Instance.settings.Sharpness = Sharpness;
        PostProcessUrp.Instance.settings.ChromaticAberration = ChromaticAberration;
        PostProcessUrp.Instance.settings.Offset = Offset;
        PostProcessUrp.Instance.settings.FishEyeDistortion = FishEyeDistortion;
        PostProcessUrp.Instance.settings.GlitchAmount = GlitchAmount;
        PostProcessUrp.Instance.settings.Distortion = Distortion;
        PostProcessUrp.Instance.settings.LensDistortion = LensDistortion;
        PostProcessUrp.Instance.settings.Vignette = Vignette;
        PostProcessUrp.Instance.settings.VignetteColor = VignetteColor;
        PostProcessUrp.Instance.settings.VignetteAmount = VignetteAmount;
        PostProcessUrp.Instance.settings.VignetteSoftness = VignetteSoftness;
        PostProcessUrp.Instance.Create();
    }
}
