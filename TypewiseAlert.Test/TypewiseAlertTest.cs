using System;
using Xunit;

namespace TypewiseAlert.Test
{
  public class TypewiseAlertTest
  {
    [Fact]
    public void InfersBreachAsPerLimits()
    {
      Assert.True(BreachChecker.inferBreach(12, 20, 30) == BreachType.TOO_LOW);
    }
  }
}
