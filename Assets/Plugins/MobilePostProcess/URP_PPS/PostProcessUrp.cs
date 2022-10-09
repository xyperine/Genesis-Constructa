namespace UnityEngine.Rendering.Universal
{
    public class PostProcessUrp : ScriptableRendererFeature
    {
        public static PostProcessUrp Instance { get; set; }

        [System.Serializable]
        public class PostProcessSettings
        {
            public RenderPassEvent Event = RenderPassEvent.AfterRenderingTransparents;

            public Material blitMaterial = null;

            public bool Blur = false;
            [Range(0, 1)]
            public float BlurAmount = 1f;
            public Texture2D BlurMask;
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
        }

        public PostProcessSettings settings = new PostProcessSettings();

        PostProcessUrpPass ppsUrpPass;

        public override void Create()
        {
            ppsUrpPass = new PostProcessUrpPass(settings.Event, settings.blitMaterial,
                settings.Blur, settings.BlurAmount, settings.BlurMask,
                settings.Bloom, settings.BloomColor, settings.BloomAmount, settings.BloomDiffuse, settings.BloomThreshold, settings.BloomSoftness,
                settings.LUT, settings.LutAmount, settings.SourceLut,
                settings.ImageFiltering, settings.Color, settings.Contrast, settings.Saturation, settings.Brightness, settings.Exposure, settings.Gamma, settings.Sharpness,
                settings.ChromaticAberration, settings.Offset, settings.FishEyeDistortion, settings.GlitchAmount,
                settings.Distortion, settings.LensDistortion,
                settings.Vignette, settings.VignetteColor, settings.VignetteAmount, settings.VignetteSoftness, this.name);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if(Instance == null)
            {
                Instance = this;
            }
            ppsUrpPass.Setup(renderer.cameraColorTarget);
            renderer.EnqueuePass(ppsUrpPass);
        }
    }
}

