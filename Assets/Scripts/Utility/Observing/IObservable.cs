using System;

namespace GenesisConstructa.Utility.Observing
{
    public interface IObservable
    {
        public event Action Changed;
    }
}