using Microsoft.AspNetCore.Identity;

namespace Workshop.Core.Interfaces
{
    public interface IPasswordHasherService
    {
        string HashPassword(string password);
        PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}
