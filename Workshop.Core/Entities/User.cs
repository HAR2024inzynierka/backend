using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.Core.Entities
{
    /// <summary>
    /// Reprezentuje użytkownika systemu.
    /// </summary>
    [Table("Uzytkownicy")]
    public class User
    {
        /// <summary>
        /// Unikalny identyfikator użytkownika.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nazwa użytkownika (login) służąca do logowania.
        /// </summary>
        public string Login { get; set; } = null!;

        /// <summary>
        /// Adres e-mail użytkownika. Musi być unikalny w systemie.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Hasło użytkownika w postaci zahaszowanej.
        /// </summary>
        [Column("Haslo")]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Numer telefonu użytkownika. Może być pusty.
        /// </summary>
        [Column("NumerTelefonu")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Rola użytkownika w systemie. Określa uprawnienia użytkownika (np. administrator, klient).
        /// </summary>
        [Column("Rola")]
        public int Role {  get; set; }
    }
}
