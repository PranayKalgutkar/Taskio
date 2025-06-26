using Taskio.App.DTOs;
using Taskio.App.IRepository;
using Taskio.App.IServices;
using Taskio.Domain.Entities;

namespace Taskio.Infra.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<object> SignUp(UserSignupDto dto)
        {
            if (await _userRepository.IsEmailExists(dto.Email))
                return "User with this email already exists.";

            var user = new User
            {
                FullName = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                UserRole = "User"
            };

            var isSaved = await _userRepository.AddUser(user);

            if (isSaved)
            {
                return new UserSignupDto
                {
                    Name = user.FullName,
                    Email = user.Email,
                    PasswordHash = "NA",
                    UserRole = user.UserRole
                };
            }
            return "Failed to create user.";
        }
    }
}