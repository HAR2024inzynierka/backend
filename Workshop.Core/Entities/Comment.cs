using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.Core.Entities
{
    /// <summary>
    /// Reprezentuje komentarz przypisany do posta w systemie.
    /// </summary>
    [Table("Komientarzy")]
    public class Comment
    {
        /// <summary>
        /// Unikalny identyfikator komentarza.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Treść komentarza.
        /// </summary>
        [Column("Tresc")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Identyfikator posta, do którego przypisany jest komentarz.
        /// </summary>
        [Column("Aktualnosci_id")]
        public int PostId { get; set; }

        /// <summary>
        /// Identyfikator użytkownika, który dodał komentarz.
        /// </summary>
        [Column("Uzytkownicy_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Data utworzenia komentarza.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Data ostatniej aktualizacji komentarza.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

    }
}
