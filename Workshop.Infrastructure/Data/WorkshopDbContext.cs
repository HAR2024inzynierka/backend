using Microsoft.EntityFrameworkCore;
using Workshop.Core.Entities;

namespace Workshop.Infrastructure.Data
{
    public class WorkshopDbContext : DbContext
    {
        public WorkshopDbContext(DbContextOptions<WorkshopDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<AutoRepairShop> AutoRepairShops { get; set; }
        public DbSet<Favour> Favours { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Record> Records { get; set; }
    }
}
