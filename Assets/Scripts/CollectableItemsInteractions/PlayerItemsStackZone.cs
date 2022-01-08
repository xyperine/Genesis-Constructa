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


        public override void TryTransferItemTo(Collector collector, StackZoneItem item)
        {
            bool collectorIsConsumer = collector is ItemsConsumer;

            if (!collectorIsConsumer)
            {
                return;
            }
            
            base.TryTransferItemTo(collector, item);
        }


        protected override bool NeedToBrakeTransferRoutine()
        {
            bool standingStill = true; // Get movement status here
            return !standingStill;
        }
    }
}