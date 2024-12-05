using Moq;
using Microsoft.AspNetCore.Identity;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Core.Services;
using Xunit;
using Workshop.Infrastructure.Repositories;

namespace Workshop.Tests.Unit.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IGenerateJwtTokenService> _generateJwtTokenServiceMock;
        private readonly Mock<IPasswordHasherService> _passwordHasherServiceMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _generateJwtTokenServiceMock = new Mock<IGenerateJwtTokenService>();
            _passwordHasherServiceMock = new Mock<IPasswordHasherService>();
            _authService = new AuthService(
                _userRepositoryMock.Object,
                _generateJwtTokenServiceMock.Object,
                _passwordHasherServiceMock.Object
            );
        }

        [Fact]
        public async Task AuthenticateAsync_WhenUserNotFound_ShouldThrowException()
        {
            var email = "nonexistent@example.com";
            var password = "password";
            var hashedPassword = "hashedPassword";

            _userRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(email))
                .ReturnsAsync((User?)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => _authService.AuthenticateAsync(email, password));

            Assert.Equal("Invalid email or password", exception.Message);

            _passwordHasherServiceMock.Verify(service => service.VerifyHashedPassword(hashedPassword, password), Times.Never);
            _generateJwtTokenServiceMock.Verify(service => service.GenerateJwtToken(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task AuthenticateAsync_WhenPasswordIsIncorrect_ShouldThrowException()
        {
            var email = "user@example.com";
            var password = "wrongpassword";

            var user = new User { 
                Id = 1, 
                Email = email, 
                PasswordHash = "hashedPassword" 
            };

            _userRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(email))
                .ReturnsAsync(user);
            _passwordHasherServiceMock
                .Setup(service => service.VerifyHashedPassword(user.PasswordHash, password))
                .Returns(PasswordVerificationResult.Failed);

            var exception = await Assert.ThrowsAsync<Exception>(() => _authService.AuthenticateAsync(email, password));

            Assert.Equal("Invalid email or password", exception.Message);

            _generateJwtTokenServiceMock.Verify(service => service.GenerateJwtToken(user), Times.Never);
        }

        [Fact]
        public async Task AuthenticateAsync_WhenCredentiolsAreValid_ShouldReturnToken()
        {
            var email = "user@example.com";
            var password = "correctpassword";
            var jwtToken = "fake-jwt-token";

            var user = new User { 
                Id = 1, 
                Email = email, 
                PasswordHash = "hashedPassword" 
            };

            _userRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(email))
                .ReturnsAsync(user);
            _passwordHasherServiceMock
                .Setup(service => service.VerifyHashedPassword(user.PasswordHash, password))
                .Returns(PasswordVerificationResult.Success);
            _generateJwtTokenServiceMock
                .Setup(service => service.GenerateJwtToken(user))
                .Returns(jwtToken);

            var result = await _authService.AuthenticateAsync(email,password);

            Assert.Equal(jwtToken, result);

            _userRepositoryMock.Verify(repo =>  repo.GetByEmailAsync(email), Times.Once());
            _passwordHasherServiceMock.Verify(service => service.VerifyHashedPassword(user.PasswordHash, password), Times.Once);
            _generateJwtTokenServiceMock.Verify(service => service.GenerateJwtToken(user), Times.Once);
        }
    }
}
