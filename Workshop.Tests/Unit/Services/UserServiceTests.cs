using Moq;
using Workshop.Infrastructure.Repositories;
using Workshop.Core.Services;
using Workshop.Core.Interfaces;
using Workshop.Core.Entities;
using Xunit;

namespace Workshop.Tests.Unit.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _vehicleRepositoryMock = new Mock<IVehicleRepository>();
            _userService = new UserService(_userRepositoryMock.Object, _vehicleRepositoryMock.Object);
        }

        [Fact]
        public async Task UpdateUserAsync_WhenUserExists_ShouldUpdateUser()
        {
            var existingUser = new User
            {
                Id = 1,
                Login = "OldLogin",
                Email = "old@example.com",
                PhoneNumber = "123456789"
            };

            var updateUser = new User
            {
                Id = 1,
                Login = "NewLogin",
                Email = "new@example.com",
                PhoneNumber = "987654321"
            };

            _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(existingUser.Id))
                .ReturnsAsync(existingUser);

            await _userService.UpdateUserAsync(updateUser);

            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(existingUser.Id), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<User>(u =>
                u.Id == updateUser.Id &&
                u.Login == updateUser.Login &&
                u.Email == updateUser.Email &&
                u.PhoneNumber == updateUser.PhoneNumber
            )), Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_WhenUserNotExists_ShouldThrowException()
        {
            var updateUser = new User
            {
                Id = 1,
                Login = "NewLogin",
                Email = "new@example.com",
                PhoneNumber = "987654321"
            };

            _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(updateUser.Id))
                .ReturnsAsync((User?)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.UpdateUserAsync(updateUser));
            Assert.Equal("User not found", exception.Message);

            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(updateUser.Id), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Never);
        }
    }
}
