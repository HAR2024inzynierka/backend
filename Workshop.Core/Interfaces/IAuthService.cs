namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs odpowiedzialny za usługi autoryzacji użytkownika.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Autoryzuje użytkownika na podstawie jego adresu e-mail i hasła.
        /// </summary>
        /// <param name="email">Adres e-mail użytkownika.</param>
        /// <param name="password">Hasło użytkownika.</param>
        /// <returns>Zwraca token autoryzacyjny w postaci ciągu znaków,
        /// jeśli dane logowania są poprawne.</returns>
        Task<string> AuthenticateAsync(string email, string password);
    }
}
