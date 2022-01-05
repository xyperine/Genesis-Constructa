using System;

namespace MoonPioneerClone.Utility.Exceptions
{
    public sealed class SameStackZoneException : ArgumentException
    {
        public SameStackZoneException() : base("Cannot set the same zone!") {  }
    }
}