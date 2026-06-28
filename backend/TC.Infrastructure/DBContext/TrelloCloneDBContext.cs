using Microsoft.EntityFrameworkCore;
using TC.Domain.Entities;

namespace TC.Infrastructure.DBContext
{
    public class TrelloCloneDBContext : DbContext
    {
        public TrelloCloneDBContext(DbContextOptions<TrelloCloneDBContext> options) : base(options)
        {
        }

        public DbSet<Board> Board { get; set; }
        public DbSet<Column> Column { get; set; }
        public DbSet<Card> Card { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}