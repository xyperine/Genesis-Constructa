using System;

namespace MoonPioneerClone.Utility.Exceptions
{
    public class SameStackZoneException : ArgumentException
    {
        public SameStackZoneException() : base("Cannot set the same zone!") {  }
    }
}