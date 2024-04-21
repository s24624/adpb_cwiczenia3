using System;
using Xunit;

namespace LegacyAppTests
{
    public class UserServiceTest
    {
        [Fact]
        public void addUser_should_return_false_when_email_is_not_correct()
        {
            string firstName = "John",
                lastName = "Doe",
                email = "incorrect";
            DateTime birthDate = DateTime.Parse("1982-03-21");
            int clinetId = 1;
            var userService = new UserService();
            
            bool result = user
            Assert.True(true);
        }
    }
}