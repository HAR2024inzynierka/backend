namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs serwisu odpowiedzialnego za rejestrację użytkownika.
    /// </summary>
    public interface IRegisterService
    {
        /// <summary>
        /// Rejestruje nowego użytkownika w systemie.
        /// </summary>
        /// <param name="login">Login użytkownika, który ma być unikalny w systemie.</param>
        /// <param name="email">Adres e-mail użytkownika, który musi być unikalny w systemie.</param>
        /// <param name="password">Hasło użytkownika, które będzie hashowane przed zapisaniem w bazie danych.</param>
        /// <returns>Zwraca token autoryzacyjny w postaci ciągu znaków.</returns>
        Task<string> RegisterUserAsync(string login, string email, string password);
    }
}
