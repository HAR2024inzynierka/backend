using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.Core.Entities
{
    [Table("Uzytkownicy")]
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Email { get; set; } = string.Empty;
        [Column("Haslo")]
        public string PasswordHash { get; set; } = string.Empty;
        [Column("NumerTelefonu")]
        public string? PhoneNumber { get; set; }
        [Column("Rola")]
        public int Role {  get; set; }
    }
}
