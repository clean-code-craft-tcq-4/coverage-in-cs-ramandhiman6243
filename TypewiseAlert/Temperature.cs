namespace TypewiseAlert
{
    public class Temperature
    {
        public struct TemperatureLimitData
        {
            public CoolingType coolingType;
            public int lowerLimit;
            public int upperLimit;

            public TemperatureLimitData(CoolingType coolingType, int lowerLimit, int upperLimit)
            {
                this.coolingType = coolingType;
                this.lowerLimit = lowerLimit;
                this.upperLimit = upperLimit;
            }
        }

        public static TemperatureLimitData[] temperatureLimits = new TemperatureLimitData[]
        {
            new TemperatureLimitData(CoolingType.PASSIVE_COOLING, 0, 35),
            new TemperatureLimitData(CoolingType.HI_ACTIVE_COOLING, 0, 45),
            new TemperatureLimitData(CoolingType.MED_ACTIVE_COOLING, 0, 40)
        };

        public static TemperatureLimitData defaultTemperatureLimit = new TemperatureLimitData(CoolingType.NONE, int.MinValue, int.MaxValue);

        public static TemperatureLimitData GetLimits(CoolingType coolingType)
        {
            for (int i = 0; i < temperatureLimits.Length; i++)
            {
                if (temperatureLimits[i].coolingType == coolingType)
                    return temperatureLimits[i];
            }

            return defaultTemperatureLimit;
        }

        public static BreachType classifyTemperatureBreach(CoolingType coolingType, double temperatureInC)
        {
            TemperatureLimitData temperatureLimit = GetLimits(coolingType);
            return BreachChecker.inferBreach(temperatureInC, temperatureLimit.lowerLimit, temperatureLimit.upperLimit);
        }
    }
}
