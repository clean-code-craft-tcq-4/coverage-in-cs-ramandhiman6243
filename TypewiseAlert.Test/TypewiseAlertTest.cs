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
        public void CheckEmailAlerterTooHigh()
        {
            string recepient = "a.b@c.com";
            string mailheaderFormat = "To: {0}\n";
            string expectedOutput = string.Format(mailheaderFormat, recepient) + "\nHi, the temperature is too high\n";

            BreachType breachType = BreachType.TOO_HIGH;

            string functionOutput = string.Empty;
            
            void PrintCallback(string message)
            {
                functionOutput = message;
            }

            EmailAlerter emailAlerter = new EmailAlerter(recepient, mailheaderFormat);
            emailAlerter.Send(breachType, PrintCallback);

            Assert.Equal(expectedOutput, functionOutput);
        }

        [Fact]
        public void CheckEmailAlerterTooLow()
        {
            string recepient = "a.b@c.com";
            string mailheaderFormat = "To: {0}\n";
            string expectedOutput = string.Format(mailheaderFormat, recepient) + "\nHi, the temperature is too low\n";

            BreachType breachType = BreachType.TOO_LOW;

            string functionOutput = string.Empty;

            void PrintCallback(string message)
            {
                functionOutput = message;
            }

            EmailAlerter emailAlerter = new EmailAlerter(recepient, mailheaderFormat);
            emailAlerter.Send(breachType, PrintCallback);

            Assert.Equal(expectedOutput, functionOutput);
        }

        [Fact]
        public void CheckEmailAlerterNormal()
        {
            string recepient = "a.b@c.com";
            string mailheaderFormat = "To: {0}\n";
            string expectedOutput = string.Empty;

            BreachType breachType = BreachType.NORMAL;

            string functionOutput = string.Empty;

            void PrintCallback(string message)
            {
                functionOutput = message;
            }

            EmailAlerter emailAlerter = new EmailAlerter(recepient, mailheaderFormat);
            emailAlerter.Send(breachType, PrintCallback);

            Assert.Equal(expectedOutput, functionOutput);
        }


        [Fact]
        public void CheckControllerAlerterTooHigh()
        {
            ushort header = 0xfeed;
            string outputFormat = "{0} : {1}\n";

            BreachType breachType = BreachType.TOO_HIGH;

            string expectedOutput = string.Format(outputFormat, header, breachType);

            string functionOutput = string.Empty;

            void PrintCallback(string message)
            {
                functionOutput = message;
            }

            ControllerAlerter controllerAlerter = new ControllerAlerter(header, outputFormat);
            controllerAlerter.Send(breachType, PrintCallback);

            Assert.Equal(expectedOutput, functionOutput);
        }

        [Fact]
        public void CheckControllerAlerterTooLow()
        {
            ushort header = 0xfeed;
            string outputFormat = "{0} : {1}\n";

            BreachType breachType = BreachType.TOO_LOW;

            string expectedOutput = string.Format(outputFormat, header, breachType);

            string functionOutput = string.Empty;

            void PrintCallback(string message)
            {
                functionOutput = message;
            }

            ControllerAlerter controllerAlerter = new ControllerAlerter(header, outputFormat);
            controllerAlerter.Send(breachType, PrintCallback);

            Assert.Equal(expectedOutput, functionOutput);
        }

        [Fact]
        public void CheckControllerAlerterNormal()
        {
            ushort header = 0xfeed;
            string outputFormat = "{0} : {1}\n";

            BreachType breachType = BreachType.NORMAL;

            string expectedOutput = string.Format(outputFormat, header, breachType);

            string functionOutput = string.Empty;

            void PrintCallback(string message)
            {
                functionOutput = message;
            }

            ControllerAlerter controllerAlerter = new ControllerAlerter(header, outputFormat);
            controllerAlerter.Send(breachType, PrintCallback);

            Assert.Equal(expectedOutput, functionOutput);
        }
    }
}
