using System;

namespace TypewiseAlert
{
    public class EmailAlerter : IAlerter
    {
        string recepient = "a.b@c.com";
        string headerFormat = "To: {0}\n";

        class BreachEmail
        {
            public BreachType breachType;
            public string message;

            public BreachEmail(BreachType breachType, string message)
            {
                this.breachType = breachType;
                this.message = message;
            }
        }

        BreachEmail[] breachEmails = new BreachEmail[]
        {
            new BreachEmail(BreachType.TOO_LOW, "Hi, the temperature is too low\n"),
            new BreachEmail(BreachType.TOO_HIGH, "Hi, the temperature is too high\n")
        };

        string GetEmailHeader(string recepient)
        {
            return string.Format(headerFormat, recepient);
        }

        bool HasBreachMessage(BreachType breachType, out string message)
        {
            for (int i = 0; i < breachEmails.Length; i++)
            {
                if (breachEmails[i].breachType == breachType)
                {
                    message = breachEmails[i].message;
                    return true;
                }
            }

            message = string.Empty;
            return false;
        }

        public void Send(BreachType breachType)
        {
            if (HasBreachMessage(breachType, out string message))
            {
                Console.WriteLine(GetEmailHeader(recepient));
                Console.WriteLine(message);
            }
        }
    }
}
