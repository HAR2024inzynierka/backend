using Microsoft.AspNetCore.Identity;
using Workshop.Core.Interfaces;
using Workshop.Core.Entities;

namespace Workshop.Core.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        public PasswordHasherService() 
        { 
            _passwordHasher = new PasswordHasher<User>();
        }

        public string HashPassword(string password)
        {
            var user = new User();
            return _passwordHasher.HashPassword(user, password);
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var user = new User();
            return _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        }
    }
}
