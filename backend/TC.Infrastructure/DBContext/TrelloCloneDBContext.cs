using Microsoft.EntityFrameworkCore;

namespace TC.Infrastructure.DBContext
{
    public class TrelloCloneDBContext : DbContext
    {
        public TrelloCloneDBContext(DbContextOptions<TrelloCloneDBContext> options) : base(options)
        {
        }
    }
}