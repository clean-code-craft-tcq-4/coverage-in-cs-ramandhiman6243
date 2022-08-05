using System;

namespace TypewiseAlert
{
    public class EmailAlerter : IAlerter
    {
        string recepient;
        string mailheaderFormat;

        public EmailAlerter(string recepient, string mailheaderFormat)
        {
            this.recepient = recepient;
            this.mailheaderFormat = mailheaderFormat;
        }

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
            return string.Format(mailheaderFormat, recepient);
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

        string CreateEmailText(string header, string message)
        {
            return header + "\n" + message;
        }

        public void Send(BreachType breachType, Action<string> printCallback)
        {
            if (HasBreachMessage(breachType, out string message))
            {
                string emailText = CreateEmailText(GetEmailHeader(recepient), message);
                printCallback(emailText);
            }
        }
    }
}
