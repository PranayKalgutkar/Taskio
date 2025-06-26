using Moq;
using Taskio.App.DTOs;
using Taskio.App.IRepository;
using Taskio.App.IServices;
using Taskio.Domain.Entities;
using Taskio.Infra.Services;

namespace Taskio.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepoMock = new();
        private readonly Mock<IEmailService> _emailServiceMock = new();
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userService = new UserService(_userRepoMock.Object, _emailServiceMock.Object);
        }

        [Fact]
        public async Task SignUpAsync_ShouldThrow_WhenEmailExists()
        {
            // Arrange
            var dto = new UserSignupDto { Name = "Test", Email = "test@example.com" };
            _userRepoMock.Setup(r => r.IsEmailExists(dto.Email)).ReturnsAsync(true);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.SignUp(dto));
            Assert.Equal("Email already exists", exception.Message);
        }

        [Fact]
        public async Task SignUpAsync_ShouldAddUserAndSendEmail_WhenValid()
        {
            // Arrange
            var dto = new UserSignupDto { Name = "John Doe", Email = "john@example.com" };
            _userRepoMock.Setup(r => r.IsEmailExists(dto.Email)).ReturnsAsync(false);
            _userRepoMock.Setup(r => r.AddUser(It.IsAny<User>())).Returns(Task.CompletedTask);
            _emailServiceMock.Setup(e => e.SendEmail(dto.Email, It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            await _userService.SignUp(dto);

            // Assert
            _userRepoMock.Verify(r => r.AddUser(It.Is<User>(u => u.Email == dto.Email && u.FullName == dto.Name)), Times.Once);
            _emailServiceMock.Verify(e => e.SendEmail(dto.Email, It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}