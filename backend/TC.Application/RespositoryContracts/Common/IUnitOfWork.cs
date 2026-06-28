using System.Threading;
using System.Threading.Tasks;

namespace TC.Application.RespositoryContracts.Common
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Begins a new transaction in the unit of work asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task BeginAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Commits the current transaction in the unit of work asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Rolls back the current transaction in the unit of work asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RollbackAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Changes the database for the current unit of work asynchronously.
        /// </summary>
        /// <param name="databaseName">The name of the database to switch to.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ChangeDatabaseAsync(string databaseName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the name of the current database in the unit of work.
        /// </summary>
        /// <returns>The name of the current database.</returns>
        string GetCurrentDatabaseName();
    }
}

