using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.Core.Entities
{
    /// <summary>
    /// Reprezentuje polubienie posta przez użytkownika.
    /// </summary>
    [Table("Polubienia")]
    public class Like
    {
        /// <summary>
        /// Unikalny identyfikator polubienia.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identyfikator posta, który został polubiony.
        /// </summary>
        [Column("Aktualnosci_id")]
        public int PostId { get; set; }

        /// <summary>
        /// Identyfikator użytkownika, który polubił post.
        /// </summary>
        [Column("Uzytkownicy_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Data i godzina, kiedy post został polubiony.
        /// </summary>
        public DateTime LikedAt { get; set; }
    }
}
