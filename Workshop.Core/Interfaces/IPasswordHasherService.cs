using Microsoft.AspNetCore.Identity;

namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs odpowiedzialny za operacje związane z haszowaniem i weryfikowaniem haseł.
    /// </summary>
    public interface IPasswordHasherService
    {
        /// <summary>
        /// Haszuje hasło użytkownika.
        /// </summary>
        /// <param name="password">Hasło, które ma zostać haszowane</param>
        /// <returns>Haszowane hasło</returns>
        string HashPassword(string password);

        /// <summary>
        /// Weryfikuje, czy podane hasło odpowiada zapisanej wersji hasła (po haszowaniu).
        /// </summary>
        /// <param name="hashedPassword">Haszowane hasło przechowywane w bazie danych</param>
        /// <param name="providedPassword">Hasło wprowadzone przez użytkownika do weryfikacji</param>
        /// <returns>Wynik weryfikacji hasła (sukces, niepoprawne hasło)</returns>
        PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}
