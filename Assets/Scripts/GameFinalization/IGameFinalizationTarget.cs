namespace ColonizationMobileGame.GameFinalization
{
    public interface IGameFinalizationTarget
    {
        void SubscribeToGameOver(GameFinalizer gameFinalizer);
    }
}