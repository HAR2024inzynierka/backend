using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.Core.Entities
{
    /// <summary>
    /// Reprezentuje post na stronie warsztatu samochodowego.
    /// </summary>
    [Table("Aktualnosci")]
    public class Post
    {
        /// <summary>
        /// Unikalny identyfikator posta.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tytuł posta.
        /// </summary>
        [Column("Tytul")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Treść posta.
        /// </summary>
        [Column("Tresc")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Identyfikator warsztatu, który opublikował post.
        /// </summary>
        [Column("Warsztat_id")]
        public int AutoRepairShopId { get; set; }

        /// <summary>
        /// Data i godzina utworzenia posta.
        /// </summary>
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Data i godzina ostatniej aktualizacji posta.
        /// </summary>
        [Column("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }


        /// <summary>
        /// Lista polubień przypisanych do posta.
        /// </summary>
        public List<Like> Likes { get; set; } = null!;

        /// <summary>
        /// Lista komentarzy przypisanych do posta.
        /// </summary>
        public List<Comment> Comments { get; set; } = null!;
    }
}
