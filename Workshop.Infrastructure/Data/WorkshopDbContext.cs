using Microsoft.EntityFrameworkCore;
using Workshop.Core.Entities;

namespace Workshop.Infrastructure.Data
{

    /// <summary>
    /// Klasa kontekstu bazy danych dla aplikacji warsztatowej.
    /// Umożliwia komunikację z bazą danych za pomocą Entity Framework Core.
    /// </summary>
    public class WorkshopDbContext : DbContext
    {
        /// <summary>
        /// Konstruktor klasy kontekstu. Używa opcji przekazanych jako parametr do konfiguracji bazy danych.
        /// </summary>
        /// <param name="options">Opcje konfiguracji dla DbContext.</param>
        public WorkshopDbContext(DbContextOptions<WorkshopDbContext> options) : base(options) { }

        /// <summary>
        /// Tabela użytkowników aplikacji.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Tabela pojazdów przypisanych do użytkowników.
        /// </summary>
        public DbSet<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Tabela warsztatów samochodowych.
        /// </summary>
        public DbSet<AutoRepairShop> AutoRepairShops { get; set; }

        /// <summary>
        /// Tabela usług oferowanych przez warsztaty.
        /// </summary>
        public DbSet<Favour> Favours { get; set; }

        /// <summary>
        /// Tabela terminów dostępnych do rezerwacji w warsztatach.
        /// </summary>
        public DbSet<Term> Terms { get; set; }

        /// <summary>
        /// Tabela zapisów na usługi w warsztatach (rezerwacje).
        /// </summary>
        public DbSet<Record> Records { get; set; }

        /// <summary>
        /// Tabela postów (np. ogłoszenia lub aktualności) związanych z warsztatami.
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        /// Tabela polubień postów przez użytkowników.
        /// </summary>
        public DbSet<Like> Likes { get; set; }

        /// <summary>
        /// Tabela komentarzy użytkowników do postów.
        /// </summary>
        public DbSet<Comment> Comments { get; set; }
    }
}
