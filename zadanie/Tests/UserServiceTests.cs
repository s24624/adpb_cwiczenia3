using System.Runtime.InteropServices.JavaScript;
using LegacyApp;

namespace Tests;

public class UserServiceTests
{
    [Fact]
    public void addUser_should_return_false_if_email_is_incorrect()
    {
        string firstName = "John",
            lastName = "Doe",
            email = "incorrect";
        DateTime dateBirth = DateTime.Parse("1982-03-21");
        int clinetId = 1;
        var service = new UserService();

        bool result = service.AddUser(firstName, lastName, email, dateBirth, clinetId);
        
        Assert.Equal(false,result);
    }
    [Fact]
    public void addUser_should_return_false_if_firstName_is_an_empty_string()
    {
        string firstName = " ",
            lastName = "Doe",
            email = "incorrect";
        DateTime dateBirth = DateTime.Parse("1982-03-21");
        int clinetId = 1;
        var service = new UserService();
        
        bool result = service.AddUser(firstName, lastName, email, dateBirth, clinetId);
        
        Assert.Equal(false,result);
    }
    
}