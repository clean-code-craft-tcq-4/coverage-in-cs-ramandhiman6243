using Xunit;

namespace TypewiseAlert.Test
{
    public class TypewiseAlertTest
    {
        private const string emailReceipent = "a.b@c.com";
        private const string mailheaderFormat = "To: {0}\n";

        public const ushort controllerHeader = 0xfeed;
        public const string controllerOutputFormat = "{0} : {1}\n";

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
            Assert.True(TypewiseAlert.checkTemperatureBreach(batteryCharacter, 36) == BreachType.TOO_HIGH);

            batteryCharacter.coolingType = CoolingType.HI_ACTIVE_COOLING;
            Assert.True(TypewiseAlert.checkTemperatureBreach(batteryCharacter, 46) == BreachType.TOO_HIGH);

            batteryCharacter.coolingType = CoolingType.MED_ACTIVE_COOLING;
            Assert.True(TypewiseAlert.checkTemperatureBreach(batteryCharacter, 41) == BreachType.TOO_HIGH);
        }

        [Fact]
        public void CheckAlerterTooLow()
        {
            BatteryCharacter batteryCharacter = new BatteryCharacter();
            batteryCharacter.coolingType = CoolingType.PASSIVE_COOLING;
            Assert.True(TypewiseAlert.checkTemperatureBreach(batteryCharacter, -1) == BreachType.TOO_LOW);

            batteryCharacter.coolingType = CoolingType.HI_ACTIVE_COOLING;
            Assert.True(TypewiseAlert.checkTemperatureBreach(batteryCharacter, -1) == BreachType.TOO_LOW);

            batteryCharacter.coolingType = CoolingType.MED_ACTIVE_COOLING;
            Assert.True(TypewiseAlert.checkTemperatureBreach(batteryCharacter, -1) == BreachType.TOO_LOW);
        }

        [Fact]
        public void CheckAlerterNormal()
        {
            BatteryCharacter batteryCharacter = new BatteryCharacter();
            batteryCharacter.coolingType = CoolingType.PASSIVE_COOLING;
            Assert.True(TypewiseAlert.checkTemperatureBreach(batteryCharacter, 0) == BreachType.NORMAL);
            Assert.True(TypewiseAlert.checkTemperatureBreach(batteryCharacter, 15) == BreachType.NORMAL);
            Assert.True(TypewiseAlert.checkTemperatureBreach(batteryCharacter, 35) == BreachType.NORMAL);

            batteryCharacter.coolingType = CoolingType.HI_ACTIVE_COOLING;
            Assert.True(TypewiseAlert.checkTemperatureBreach(batteryCharacter, 0) == BreachType.NORMAL);
            Assert.True(TypewiseAlert.checkTemperatureBreach(batteryCharacter, 25) == BreachType.NORMAL);
            Assert.True(TypewiseAlert.checkTemperatureBreach(batteryCharacter, 45) == BreachType.NORMAL);

            batteryCharacter.coolingType = CoolingType.MED_ACTIVE_COOLING;
            Assert.True(TypewiseAlert.checkTemperatureBreach(batteryCharacter, 0) == BreachType.NORMAL);
            Assert.True(TypewiseAlert.checkTemperatureBreach(batteryCharacter, 20) == BreachType.NORMAL);
            Assert.True(TypewiseAlert.checkTemperatureBreach(batteryCharacter, 40) == BreachType.NORMAL);
        }

        [Fact]
        public void CheckAlerterNoneCoolingType()
        {
            BatteryCharacter batteryCharacter = new BatteryCharacter();
            batteryCharacter.coolingType = CoolingType.NONE;
            Assert.True(TypewiseAlert.checkTemperatureBreach(batteryCharacter, 0) == BreachType.NORMAL);
        }


        [Fact]
        public void CheckTypewiseAlerter()
        {
            EmailAlerter emailAlerter = new EmailAlerter(emailReceipent, mailheaderFormat);

            BatteryCharacter batteryCharacter = new BatteryCharacter();
            batteryCharacter.coolingType = CoolingType.PASSIVE_COOLING;

            string expectedOutput = "Checking " + BreachType.TOO_HIGH;
            string actualOutput = string.Empty;

            TypewiseAlert.checkAndAlert(emailAlerter, batteryCharacter, 60, output => actualOutput = output);

            Assert.Equal(expectedOutput, actualOutput);
        }


        void CheckAlerter(IAlerter alerter, string expectedOutput, BreachType breachType)
        {
            string actualOutput = string.Empty;
            alerter.Send(breachType, output => actualOutput = output);
            Assert.Equal(expectedOutput, actualOutput);
        }

        void CheckEmailAlerter(string messageText, BreachType breachType)
        {
            EmailAlerter emailAlerter = new EmailAlerter(emailReceipent, mailheaderFormat);
            CheckAlerter(emailAlerter, messageText, breachType);
        }

        [Fact]
        public void CheckEmailAlerterTooHigh()
        {
            string messageText = string.Format(mailheaderFormat, emailReceipent) + "\nHi, the temperature is too high\n";
            BreachType breachType = BreachType.TOO_HIGH;
            CheckEmailAlerter(messageText, breachType);
        }

        [Fact]
        public void CheckEmailAlerterTooLow()
        {
            string messageText = string.Format(mailheaderFormat, emailReceipent) + "\nHi, the temperature is too low\n";
            BreachType breachType = BreachType.TOO_LOW;
            CheckEmailAlerter(messageText, breachType);
        }

        [Fact]
        public void CheckEmailAlerterNormal()
        {
            string messageText = string.Empty;
            BreachType breachType = BreachType.NORMAL;
            CheckEmailAlerter(messageText, breachType);
        }


        void CheckControllerAlerter(BreachType breachType)
        {
            string expectedOuput = string.Format(controllerOutputFormat, controllerHeader, breachType);
            ControllerAlerter controllerAlerter = new ControllerAlerter(controllerHeader, controllerOutputFormat);
            CheckAlerter(controllerAlerter, expectedOuput, breachType);
        }

        [Fact]
        public void CheckControllerAlerterTooHigh()
        {
            BreachType breachType = BreachType.TOO_HIGH;
            CheckControllerAlerter(breachType);
        }

        [Fact]
        public void CheckControllerAlerterTooLow()
        {
            BreachType breachType = BreachType.TOO_LOW;
            CheckControllerAlerter(breachType);
        }

        [Fact]
        public void CheckControllerAlerterNormal()
        {
            BreachType breachType = BreachType.NORMAL;
            CheckControllerAlerter(breachType);
        }
    }
}
