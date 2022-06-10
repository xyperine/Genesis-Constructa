using System;

namespace ColonizationMobileGame.Utility.Observing
{
    public interface IObservable
    {
        public event Action Changed;
    }
}