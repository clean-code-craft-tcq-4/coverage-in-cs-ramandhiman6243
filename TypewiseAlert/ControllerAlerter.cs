using System;

namespace TypewiseAlert
{
    public class ControllerAlerter : IAlerter
    {
        ushort header;
        string outputFormat;

        public ControllerAlerter(ushort header, string outputFormat)
        {
            this.header = header;
            this.outputFormat = outputFormat;
        }

        public void Send(BreachType breachType, Action<string> printCallback)
        {
            string output = string.Format(outputFormat, header, breachType);
            printCallback(output);
        }
    }
}
