namespace TypewiseAlert
{
    public class Alerter
    {
        public static void checkAndAlert(IAlerter alertTarget, BatteryCharacter batteryChar, double temperatureInC)
        {
            BreachType breachType = Temperature.classifyTemperatureBreach(batteryChar.coolingType, temperatureInC);
            alertTarget.Send(breachType);
        }
    }
}
