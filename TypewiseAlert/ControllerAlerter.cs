using System;

namespace TypewiseAlert
{
    public class ControllerAlerter : IAlerter
    {
        public void Send(BreachType breachType)
        {
            const ushort header = 0xfeed;
            Console.WriteLine("{} : {}\n", header, breachType);
        }
    }
}
