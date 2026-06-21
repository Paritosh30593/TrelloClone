using TC.Application.RespositoryContracts;
using TC.Infrastructure.DBContext;

namespace TC.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TrelloCloneDBContext _context;

        public UnitOfWork(TrelloCloneDBContext context)
        {
            _context = context;
        }

        public Task Save(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}