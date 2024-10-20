using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string email, string password);
    }
}
