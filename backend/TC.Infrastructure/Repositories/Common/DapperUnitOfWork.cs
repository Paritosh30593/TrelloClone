using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using TC.Application.RespositoryContracts.Common;

namespace TC.Infrastructure.Repositories.Common
{
    public class DapperUnitOfWork : IDapperUnitOfWork
    {
        private DbConnection _db;

        private DbTransaction _tx;

        public DbConnection DBConnection => _db;

        public DbTransaction DBTransaction => _tx;

        public DapperUnitOfWork(DbProviderFactory dbProviderFactory, string connectionString)
        {
            _db = dbProviderFactory.CreateConnection()!;
            _db.ConnectionString = connectionString;
        }

        public async Task BeginAsync(CancellationToken cancellationToken = default)
        {
            if (_tx is not null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            if (_db.State == System.Data.ConnectionState.Closed)
            {
                await _db.OpenAsync(cancellationToken);
            }

            _tx = await _db.BeginTransactionAsync(cancellationToken);
        }

        public async Task ChangeDatabaseAsync(string databaseName, CancellationToken cancellationToken = default)
        {
            if (_db.State == System.Data.ConnectionState.Closed)
            {
                await _db.OpenAsync(cancellationToken);
            }

            await _db.ChangeDatabaseAsync(databaseName, cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_tx is null)
            {
                throw new InvalidOperationException("No transaction in progress to commit.");
            }

            await _tx.CommitAsync(cancellationToken);
            await _tx.DisposeAsync();
            _tx = null;
            await _db.CloseAsync();
        }

        public string GetCurrentDatabaseName()
        {
            return _db.Database;
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_tx is null)
            {
                throw new InvalidOperationException("No transaction in progress to rollback.");
            }

            await _tx.RollbackAsync(cancellationToken);
            await _tx.DisposeAsync();
            _tx = null;
            await _db.CloseAsync();
        }
    }
}