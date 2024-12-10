using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs definiujący operacje na encji Like.
    /// Reprezentuje zarządzanie polubieniami postów przez użytkowników.
    /// </summary>
    public interface ILikeRepository
    {
        /// <summary>
        /// Pobiera polubienie na podstawie identyfikatora użytkownika i identyfikatora posta.
        /// </summary>
        /// <param name="userId">Identyfikator użytkownika.</param>
        /// /// <param name="postId">Identyfikator posta.</param>
        /// <returns>Obiekt Like lub null, jeśli nie znaleziono polubienia.</returns>
        Task<Like?> GetLikeByUserIdAndPostIdAsync(int userId, int postId);

        /// <summary>
        /// Dodaje nowe polubienie do bazy danych.
        /// </summary>
        /// <param name="like">Obiekt Like do dodania.</param>
        Task AddLikeAsync(Like like);

        /// <summary>
        /// Usuwa polubienie z bazy danych.
        /// </summary>
        /// <param name="like">Obiekt Like do usunięcia.</param>
        Task DeleteLikeAsync(Like like);

        /// <summary>
        /// Pobiera liczbę polubień przypisanych do konkretnego posta.
        /// </summary>
        /// <param name="postsId">Identyfikator posta.</param>
        /// <returns>Liczba polubień przypisanych do posta.</returns>
        Task<int> GetLikeCountByPostIdAsync(int postsId);

        /// <summary>
        /// Sprawdza, czy dany użytkownik polubił określony post.
        /// </summary>
        /// <param name="userId">Identyfikator użytkownika.</param>
        /// <param name="postId">Identyfikator posta.</param>
        /// <returns>True, jeśli użytkownik polubił post; False w przeciwnym razie.</returns>
        Task<bool> IsUserLikedPostAsync(int userId, int postId);
        
    }
}
