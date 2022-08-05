namespace TypewiseAlert
{
    public class Alerter
    {
        public static void checkAndAlert(IAlerter alertTarget, BatteryCharacter batteryChar, double temperatureInC)
        {
            BreachType breachType = checkTemperatureBreach(batteryChar, temperatureInC);
            alertTarget.Send(breachType);
        }

        public static BreachType checkTemperatureBreach(BatteryCharacter batteryChar, double temperatureInC)
        {
            BreachType breachType = Temperature.classifyTemperatureBreach(batteryChar.coolingType, temperatureInC);
            return breachType;
        }
    }
}
