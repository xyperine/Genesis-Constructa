namespace ColonizationMobileGame.UI.ArrowPointers.TargetGroupValidators.Regular
{
    public record RegularTargetValidationData
    {
        public float OnScreenDuration { get; set; }
        public float OffScreenDuration { get; set; }
        public bool Valid { get; set; } = true;
    }
}