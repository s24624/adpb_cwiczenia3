using System;
using System.Text.RegularExpressions;

namespace LegacyApp
{
    public class UserService

    {
        public bool validateNameAndSurname(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            return true;
        }

        public bool isValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            return Regex.IsMatch(email, pattern);
        }

        public int getAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            {
                age--;
            }

            return age;
        }

        public bool checkAge(DateTime dateOfBirth)
        {
            if (getAge(dateOfBirth) < 21)
            {
                return false;
            }

            return true;
        }

        public void checkClientType(ClientRepository clientRepository, User user, int clientId)
        {
            var client = clientRepository.GetById(clientId);
            if (client.Type.Equals("VeryImportantClient"))
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type.Equals("ImportantClient"))
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }
        }

        public bool checkCreditLimit(User user)
        {
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            return true;
        }

        public User createUser(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
        {
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            return user;
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (validateNameAndSurname(firstName, lastName) && isValidEmail(email) && checkAge(dateOfBirth))
            {
                var clientRepository = new ClientRepository();
                var client = clientRepository.GetById(clientId);
                var user = createUser(firstName, lastName, email, dateOfBirth, client);

                checkClientType(clientRepository, user, clientId);
                if (!checkCreditLimit(user)) return false;

                UserDataAccess.AddUser(user);
                return true;
            }

            return false;
        }
    }

}
