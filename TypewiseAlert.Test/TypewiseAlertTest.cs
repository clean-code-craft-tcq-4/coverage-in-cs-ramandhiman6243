using Xunit;

namespace TypewiseAlert.Test
{
    public class TypewiseAlertTest
    {
        [Fact]
        public void InfersBreachLow()
        {
            Assert.True(BreachChecker.inferBreach(19, 20, 30) == BreachType.TOO_LOW);
        }

        [Fact]
        public void InfersBreachNormal()
        {
            Assert.True(BreachChecker.inferBreach(20, 20, 30) == BreachType.NORMAL);
            Assert.True(BreachChecker.inferBreach(21, 20, 30) == BreachType.NORMAL);
            Assert.True(BreachChecker.inferBreach(29, 20, 30) == BreachType.NORMAL);
            Assert.True(BreachChecker.inferBreach(30, 20, 30) == BreachType.NORMAL);
        }

        [Fact]
        public void InfersBreachHigh()
        {
            Assert.True(BreachChecker.inferBreach(31, 20, 30) == BreachType.TOO_HIGH);
        }


        [Fact]
        public void CheckAlerterTooHigh()
        {
            BatteryCharacter batteryCharacter = new BatteryCharacter();
            batteryCharacter.coolingType = CoolingType.PASSIVE_COOLING;
            Assert.True(Alerter.checkTemperatureBreach(batteryCharacter, 36) == BreachType.TOO_HIGH);

            batteryCharacter.coolingType = CoolingType.HI_ACTIVE_COOLING;
            Assert.True(Alerter.checkTemperatureBreach(batteryCharacter, 46) == BreachType.TOO_HIGH);

            batteryCharacter.coolingType = CoolingType.MED_ACTIVE_COOLING;
            Assert.True(Alerter.checkTemperatureBreach(batteryCharacter, 41) == BreachType.TOO_HIGH);
        }

        [Fact]
        public void CheckAlerterTooLow()
        {
            BatteryCharacter batteryCharacter = new BatteryCharacter();
            batteryCharacter.coolingType = CoolingType.PASSIVE_COOLING;
            Assert.True(Alerter.checkTemperatureBreach(batteryCharacter, -1) == BreachType.TOO_LOW);

            batteryCharacter.coolingType = CoolingType.HI_ACTIVE_COOLING;
            Assert.True(Alerter.checkTemperatureBreach(batteryCharacter, -1) == BreachType.TOO_LOW);

            batteryCharacter.coolingType = CoolingType.MED_ACTIVE_COOLING;
            Assert.True(Alerter.checkTemperatureBreach(batteryCharacter, -1) == BreachType.TOO_LOW);
        }

        [Fact]
        public void CheckAlerterNormal()
        {
            BatteryCharacter batteryCharacter = new BatteryCharacter();
            batteryCharacter.coolingType = CoolingType.PASSIVE_COOLING;
            Assert.True(Alerter.checkTemperatureBreach(batteryCharacter, 0) == BreachType.NORMAL);
            Assert.True(Alerter.checkTemperatureBreach(batteryCharacter, 15) == BreachType.NORMAL);
            Assert.True(Alerter.checkTemperatureBreach(batteryCharacter, 35) == BreachType.NORMAL);

            batteryCharacter.coolingType = CoolingType.HI_ACTIVE_COOLING;
            Assert.True(Alerter.checkTemperatureBreach(batteryCharacter, 0) == BreachType.NORMAL);
            Assert.True(Alerter.checkTemperatureBreach(batteryCharacter, 25) == BreachType.NORMAL);
            Assert.True(Alerter.checkTemperatureBreach(batteryCharacter, 45) == BreachType.NORMAL);

            batteryCharacter.coolingType = CoolingType.MED_ACTIVE_COOLING;
            Assert.True(Alerter.checkTemperatureBreach(batteryCharacter, 0) == BreachType.NORMAL);
            Assert.True(Alerter.checkTemperatureBreach(batteryCharacter, 20) == BreachType.NORMAL);
            Assert.True(Alerter.checkTemperatureBreach(batteryCharacter, 40) == BreachType.NORMAL);
        }


        [Fact]
        public void CheckEmailAlerterTooHigh()
        {
            //In progress
        }

        [Fact]
        public void CheckEmailAlerterTooLow()
        {
            //In progress
        }

        [Fact]
        public void CheckEmailAlerterNormal()
        {
            //In progress
        }


        [Fact]
        public void CheckControllerAlerterTooHigh()
        {
            //In progress
        }

        [Fact]
        public void CheckControllerAlerterTooLow()
        {
            //In progress
        }

        [Fact]
        public void CheckControllerAlerterNormal()
        {
            //In progress
        }
    }
}
