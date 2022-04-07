using System;

namespace MoonPioneerClone.Utility.Observing
{
    public interface IObservable
    {
        public event Action Changed;
    }
}