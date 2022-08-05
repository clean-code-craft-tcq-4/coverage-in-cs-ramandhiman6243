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
