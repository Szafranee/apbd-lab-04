using System;

namespace LegacyApp
{
    public class UserService
    {

        public static bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {

            if (!UserAddValidator.IsFirstNameCorrect(firstName) || !UserAddValidator.IsLastnameCorrect(lastName))
            {
                return false;
            }

            if (!UserAddValidator.IsEmailCorrect(email)) return false;

            if (!UserAddValidator.IsAgeCorrect(dateOfBirth)) return false;

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            UserAddValidator.CalculateCreditLimit(client, user);

            if (!UserAddValidator.IsCreditLimitValid(user, 500)) return false;

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
