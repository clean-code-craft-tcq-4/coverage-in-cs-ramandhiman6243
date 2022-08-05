using System;

namespace TypewiseAlert
{
    public interface IAlerter
    {
        void Send(BreachType breachType, Action<string> printCallback);
    };
}
