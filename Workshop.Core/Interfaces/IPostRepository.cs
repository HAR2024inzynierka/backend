using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs definiujący operacje na encji Post.
    /// Reprezentuje zarządzanie postami użytkowników w systemie warsztatów samochodowych.
    /// </summary>
    public interface IPostRepository
    {
        /// <summary>
        /// Pobiera post na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator posta.</param>
        /// <returns>Obiekt Post lub null, jeśli post nie został znaleziony.</returns>
        Task<Post?> GetPostByIdAsync(int id);

        /// <summary>
        /// Dodaje nowy post do bazy danych.
        /// </summary>
        /// <param name="post">Obiekt Post do dodania.</param>
        Task AddPostAsync(Post post);

        /// <summary>
        /// Aktualizuje istniejący post w bazie danych.
        /// </summary>
        /// <param name="post">Obiekt Post z zaktualizowanymi danymi.</param>
        Task UpdatePostAsync(Post post);

        /// <summary>
        /// Usuwa post z bazy danych.
        /// </summary>
        /// <param name="post">Obiekt Post do usunięcia.</param>
        Task DeletePostAsync(Post post);

        /// <summary>
        /// Pobiera wszystkie posty w systemie.
        /// </summary>
        /// <returns>Lista wszystkich postów.</returns>
        Task<List<Post>> GetAllPostsAsync();

        /// <summary>
        /// Pobiera wszystkie posty związane z konkretnym warsztatem samochodowym.
        /// </summary>
        /// <param name="autoRepairShopId">Identyfikator warsztatu samochodowego.</param>
        /// <returns>Lista postów przypisanych do wskazanego warsztatu.</returns>
        Task<List<Post>> GetPostsByAutoServiceIdAsync(int autoRepairShopId);
    }
}
