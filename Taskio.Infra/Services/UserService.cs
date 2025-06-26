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

        public async Task SignUp(UserSignupDto dto)
        {
            if (await _userRepository.IsEmailExists(dto.Email))
                throw new Exception("Email already exists");

            var user = new User { FullName = dto.Name, Email = dto.Email, PasswordHash = dto.PasswordHash };

            await _userRepository.AddUser(user);
            await _emailService.SendEmail(user.Email, "Welcome", "Thanks for signing up!");
        }
    }
}