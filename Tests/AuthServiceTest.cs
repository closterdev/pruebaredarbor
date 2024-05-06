using Application.Dtos;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Ports;
using Microsoft.Extensions.Configuration;
using Moq;
using Shared.Common;

namespace Tests
{
    public class AuthServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthService _authService;

        public AuthServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _tokenServiceMock = new Mock<ITokenService>();
            _authService = new AuthService(_userRepositoryMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public async Task ValidateUser_UserExistsAndIsActive_ReturnsValidToken()
        {
            // Arrange
            var userCredentials = new TokenIn { Username = "javierski", Password = "javierski" };
            var user = new User { Username = "javierski", Password = "amF2aWVyc2tp", IsActive = true };
            _userRepositoryMock.Setup(repo => repo.GetUserByKeyAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Configurar mock para generación de token JWT
            _configurationMock.SetupGet(config => config["CredentialsJwt:Key"]).Returns("this is my custom Secret key for authentication");
            _configurationMock.SetupGet(config => config["CredentialsJwt:Issuer"]).Returns("Jmartinezqu");
            _configurationMock.SetupGet(config => config["CredentialsJwt:Audience"]).Returns("Yolo");

            _tokenServiceMock.Setup(service => service.JwtToken()).Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MTQ5Mzk1NDksImlzcyI6IkptYXJ0aW5lenF1IiwiYXVkIjoiWW9sbyJ9.dgWKV--5TwLhYEjevs5MO7VweCxXlpeSLZdtzvAdi0s");

            // Act
            var result = await _authService.ValidateUser(userCredentials);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Result.Success, result.Result);
            Assert.NotNull(result.Token);
        }

        [Fact]
        public async Task ValidateUser_UserDoesNotExist_ReturnsNoRecords()
        {
            // Arrange
            var userCredentials = new TokenIn { Username = "jmartinez1", Password = "password" };
            _userRepositoryMock.Setup(repo => repo.GetUserByKeyAsync(It.IsAny<User>())).ReturnsAsync((User?)null);

            // Act
            var result = await _authService.ValidateUser(userCredentials);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Result.NoRecords, result.Result);
            Assert.Null(result.Token);
        }

        [Fact]
        public async Task ValidateUser_UserExist_ReturnsIsNotActive()
        {
            // Arrange
            var userCredentials = new TokenIn { Username = "javierskiv2", Password = "javierski" };
            var user = new User { Username = "javierskiv2", Password = "amF2aWVyc2tp", IsActive = false };
            _userRepositoryMock.Setup(repo => repo.GetUserByKeyAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _authService.ValidateUser(userCredentials);

            // Assert
            Assert.Equal(Result.IsNotActive, result.Result);
        }

        [Fact]
        public async Task ValidateUser_UserExist_ReturnsInvalidPassword()
        {
            // Arrange
            var userCredentials = new TokenIn { Username = "javierski", Password = "javierskiv2" };
            var user = new User { Username = "javierski", Password = "amF2aWVyc2tp", IsActive = true };
            _userRepositoryMock.Setup(repo => repo.GetUserByKeyAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _authService.ValidateUser(userCredentials);

            // Assert
            Assert.Equal(Result.InvalidPassword, result.Result);
        }
    }

}