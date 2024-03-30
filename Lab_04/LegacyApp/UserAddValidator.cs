using System;

namespace LegacyApp;

public static class UserAddValidator
{
    public static bool IsCreditLimitValid(User user, int minCreditLimit)
    {
        return !user.HasCreditLimit || user.CreditLimit >= minCreditLimit;
    }

    public static void CalculateCreditLimit(Client client, User user)
    {
        if (client.Type == "VeryImportantClient")
        {
            user.HasCreditLimit = false;
        }
        else
        {
            user.HasCreditLimit = true;
            using var userCreditService = new UserCreditService();
            var creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);

            if (client.Type == "ImportantClient")
            {
                creditLimit *= 2;
            }
            user.CreditLimit = creditLimit;
        }
    }

    public static bool IsAgeCorrect(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        var age = now.Year - dateOfBirth.Year;
        if (now.AddYears(-age) < dateOfBirth) age--;

        return age >= 21;
    }

    internal static bool IsEmailCorrect(string email)
    {
        return email.Contains('@') || email.Contains('.');
    }

    public static bool IsLastnameCorrect(string lastName)
    {
        return !string.IsNullOrEmpty(lastName);
    }

    public static bool IsFirstNameCorrect(string firstName)
    {
        return !string.IsNullOrEmpty(firstName);
    }
}