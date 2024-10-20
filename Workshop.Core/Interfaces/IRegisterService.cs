

namespace Workshop.Core.Interfaces
{
    public  interface IRegisterService
    {
        Task<string> RegisterUserAsync(string login, string email, string password);
    }
}
