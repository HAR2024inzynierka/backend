using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs definiujący operacje na komentarzach.
    /// Odpowiada za interakcje z encją Comment w kontekście bazy danych.
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Pobiera komentarz na podstawie identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator komentarza.</param>
        /// <returns>Obiekt Comment lub null, jeśli nie znaleziono.</returns>
        Task<Comment?> GetCommentByIdAsync(int id);

        /// <summary>
        /// Pobiera wszystkie komentarze przypisane do konkretnego posta.
        /// </summary>
        /// <param name="postId">Identyfikator posta, do którego przypisane są komentarze.</param>
        /// <returns>Lista obiektów Comment.</returns>
        Task<List<Comment>> GetAllCommentsByPostIdAsync(int postId);

        /// <summary>
        /// Dodaje nowy komentarz do bazy danych.
        /// </summary>
        /// <param name="comment">Obiekt Comment do dodania.</param>
        Task AddCommentAsync(Comment comment);

        /// <summary>
        /// Aktualizuje istniejący komentarz w bazie danych.
        /// </summary>
        /// <param name="comment">Obiekt Comment z wprowadzonymi zmianami.</param>
        Task UpdateCommentAsync(Comment comment);

        /// <summary>
        /// Usuwa komentarz z bazy danych.
        /// </summary>
        /// <param name="comment">Obiekt Comment do usunięcia.</param>
        Task DeleteCommentAsync(Comment comment);
    }
}
