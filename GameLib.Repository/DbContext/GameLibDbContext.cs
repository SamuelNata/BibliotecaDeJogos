using GameLib.Model.DTOs;
using GameLib.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace GameLib.Repository.DbContext
{
    public class GameLibDbContext : BaseDbContext
    {
        public GameLibDbContext(DbContextOptions<GameLibDbContext> options):base(options) {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // To query a non-entity class, add the configuration here
            // modelBuilder.Entity<GameBorrowingDTO>().HasNoKey();
        }

        public DbSet<User> User { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<GameBorrowing> GameBorrowing { get; set; }
        public DbSet<UserGame> UserGame { get; set; }
    }
}