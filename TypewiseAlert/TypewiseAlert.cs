using System;

namespace TypewiseAlert
{
    public class TypewiseAlert
    {
        public static void checkAndAlert(IAlerter alertTarget, BatteryCharacter batteryChar, double temperatureInC, Action<string> printCallback)
        {
            BreachType breachType = checkTemperatureBreach(batteryChar, temperatureInC);
            alertTarget.Send(breachType, printCallback);
            printCallback("Checking " + breachType);
        }

        public static BreachType checkTemperatureBreach(BatteryCharacter batteryChar, double temperatureInC)
        {
            BreachType breachType = Temperature.classifyTemperatureBreach(batteryChar.coolingType, temperatureInC);
            return breachType;
        }
    }
}
