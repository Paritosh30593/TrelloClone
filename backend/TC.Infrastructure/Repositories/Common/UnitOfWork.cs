using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TC.Application.RespositoryContracts.Common;
using TC.Infrastructure.DBContext;

namespace TC.Infrastructure.Repositories.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TrelloCloneDBContext _context;

        public UnitOfWork(TrelloCloneDBContext context)
        {
            _context = context;
        }

        public Task BeginAsync(CancellationToken cancellationToken = default) => _context.Database.BeginTransactionAsync(cancellationToken);

        public Task CommitAsync(CancellationToken cancellationToken = default) => _context.Database.CommitTransactionAsync(cancellationToken);

        public Task RollbackAsync(CancellationToken cancellationToken = default) => _context.Database.RollbackTransactionAsync(cancellationToken);

        public async Task ChangeDatabaseAsync(string databaseName, CancellationToken cancellationToken = default)
        {
            var connection = _context.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
            {
                await connection.OpenAsync(cancellationToken);
            }
            await connection.ChangeDatabaseAsync(databaseName, cancellationToken);
        }

        public string GetCurrentDatabaseName() => _context.Database.GetDbConnection().Database;
    }
}