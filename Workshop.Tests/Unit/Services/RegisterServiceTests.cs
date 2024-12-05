using Moq;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Core.Services;
using Workshop.Infrastructure.Repositories;
using Xunit;

namespace Workshop.Tests.Unit.Services
{
    public class RegisterServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IGenerateJwtTokenService> _generateJwtTokenServiceMock;
        private readonly Mock<IPasswordHasherService> _passwordHasherServiceMock;
        private readonly IRegisterService _registerService;

        public RegisterServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _generateJwtTokenServiceMock = new Mock<IGenerateJwtTokenService>();
            _passwordHasherServiceMock = new Mock<IPasswordHasherService>();
            _registerService = new RegisterService(
                _userRepositoryMock.Object,
                _generateJwtTokenServiceMock.Object,
                _passwordHasherServiceMock.Object
            );
        }

        [Fact]
        public async Task RegisterUserAsync_WhenEmailIsAlreadyUsed_ShouldThrowException()
        {
            var email = "existing@example.com";
            _userRepositoryMock
                .Setup(repo => repo.EmailExistsAsync( email ))
                .ReturnsAsync(true);

            var exception = await Assert.ThrowsAnyAsync<Exception>(() => _registerService.RegisterUserAsync("user1", email, "password"));

            Assert.Equal("Email is already in use.", exception.Message);

            _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task RegisterUserAsync_WhenUserIsSuccessfullyRegistered_ShouldRerturnToken()
        {
            var login = "newuser";
            var email = "newuser@example.com";
            var password = "password";
            var hashedPassword = "hashedPassword";
            var jwtToken = "fake-jwt-token";

            _userRepositoryMock
                .Setup(repo => repo.EmailExistsAsync(email))
                .ReturnsAsync(false);
            _passwordHasherServiceMock
                .Setup(service => service.HashPassword(password))
                .Returns(hashedPassword);
            _userRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);
            _generateJwtTokenServiceMock
                .Setup(service => service.GenerateJwtToken(It.IsAny<User>()))
                .Returns(jwtToken);

            var result = await _registerService.RegisterUserAsync(login, email, password);

            Assert.Equal(jwtToken, result);

            _userRepositoryMock.Verify(repo => repo.EmailExistsAsync(email), Times.Once);
            _passwordHasherServiceMock.Verify(service => service.HashPassword(password), Times.Once);
            _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once);
            _generateJwtTokenServiceMock.Verify(service => service.GenerateJwtToken(It.IsAny<User>()), Times.Once);
        }
    }
}
