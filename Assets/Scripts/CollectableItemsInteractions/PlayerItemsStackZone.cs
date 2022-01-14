namespace MoonPioneerClone.CollectableItemsInteractions
{
    public sealed class PlayerItemsStackZone : ItemsStackZone
    {
        public override void TryTransferTo(Collector collector)
        {
            bool standingStill = true; // Get movement status here

            if (!standingStill)
            {
                return;
            }
            
            base.TryTransferTo(collector);
        }


        protected override bool NeedToBrakeTransferRoutine()
        {
            bool standingStill = true; // Get movement status here
            return !standingStill;
        }
    }
}