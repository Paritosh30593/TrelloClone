using TC.Application.RespositoryContracts;
using TC.Infrastructure.DBContext;

namespace TC.Infrastructure.Repositories
{
    public class UnitOfWork(TrelloCloneDBContext context) : IUnitOfWork
    {
        private readonly TrelloCloneDBContext _context = context;

        public Task Save(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}