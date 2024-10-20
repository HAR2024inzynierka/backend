using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> Authenticate(string email, string password);
    }
}
