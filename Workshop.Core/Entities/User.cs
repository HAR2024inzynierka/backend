using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.Core.Entities
{
    [Table("Uzytkownicy")]
    public class User
    {
        public int Id { get; set; }
        public string Login {  get; set; }
        public string Email { get; set; }
        [Column("Haslo")]
        public string PasswordHash { get; set; }
        [Column("NumerTelefonu")]
        public string PhoneNumber { get; set; }
        [Column("Rola")]
        public int Role {  get; set; }
    }
}
