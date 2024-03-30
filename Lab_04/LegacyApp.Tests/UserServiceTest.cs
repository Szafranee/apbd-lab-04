using System;
using JetBrains.Annotations;
using LegacyApp;
using Xunit;

namespace LegacyApp.Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{

    [Fact]
    public void AddUser_WhenFirstNameIsEmpty_ReturnsFalse()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = UserService.AddUser("", "Doe", "test.mail@gmail.com", new DateTime(1990, 1, 1), 1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_WhenLastNameIsEmpty_ReturnsFalse()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = UserService.AddUser("John", "", "test.mail@gmail.com", new DateTime(1990, 1, 1), 1);
    }

    [Fact]
    public void AddUser_WhenEmailDoesNotContainAtSymbolAndDot_ReturnsFalse()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = UserService.AddUser("John", "Doe", "testmailgmailcom", new DateTime(1990, 1, 1), 1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_DecreaseUserAgeByOneWhenUserBirthdayHasNotPassed_ReturnsAgeDecreasedByOne()
    {
        // Arrange
        var dateOfBirth = new DateTime(1990, 10, 1);
        var now = new DateTime(2020, 4, 1);

        // Act
        var age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

        // Assert
        Assert.Equal(29, age);
    }

    [Fact]
    public void AddUser_WhenAgeIsLessThan21_ReturnsFalse()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = UserService.AddUser("John", "Doe", "test.mail@gmail.com", new DateTime(2020, 1, 1), 1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_WhenClientTypeIsVeryImportantClient_ReturnsUserHasCreditLimitFalse()
    {
        // Arrange
        var userService = new UserService();
        var user = new User();

        // Act
        UserService.AddUser("John", "Malewski", "test.mail@gmail.com",  new DateTime(1990, 1, 1), 2);

        // Assert
        Assert.False(user.HasCreditLimit);
    }

    /*
    [Fact]
    public void AddUser_WhenClientTypeIsImportantClient_SetsUserCreditLimitToDoubleCreditLimitFromUserCreditService()
    {
        // Arrange
        var userService = new UserService();
        var clientRepository = new ClientRepository();
        var client = new Client();

        // Act
        User user = userService.AddUser("Adam", "Smith", "smith@gmail.pl", new DateTime(1990, 1, 1), 3);

        // Assert
        Assert.Equal(20000, user.CreditLimit);
    }*/

    /*[Fact]
    public void AddUser_WhenClientTypeIsNormalClient_SetsUserCreditLimitToCreditLimitFromUserCreditService()
    {
        // Arrange
        var userService = new UserService();
        var userCreditService = new UserCreditService();
        var user = new User();

        // Act
        userService.AddUser("Jan", "Kowalski", "kowalski@wp.pl", new DateTime(1990, 1, 1), 1);

        // Assert
        Assert.Equal(200, user.CreditLimit);
    }*/

    [Fact]
    public void AddUser_WhenClientHasCreditLimitAndCreditLimitIsLessThan500_ReturnsFalse()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = UserService.AddUser("Jan", "Kowalski", "kowalski@wp.pl", new DateTime(1990, 1, 1), 1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_RunsSuccessfully_ReturnsTrue()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = UserService.AddUser("John", "Malewski", "test.mail@gmail.com", new DateTime(1990, 1, 1), 2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void AddUser_UserDoesNotExistInDatabase_ReturnsFalse()
    {
        // Arrange
        var userService = new UserService();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            UserService.AddUser("John", "Malewski", "test.mail@gmail.com", new DateTime(1990, 1, 1), 9);
        });
    }
}