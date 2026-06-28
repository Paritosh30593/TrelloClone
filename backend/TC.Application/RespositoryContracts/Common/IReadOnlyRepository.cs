using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TC.Application.RespositoryContracts.Common
{
    public interface IReadOnlyRepository<T> where T : class
    {
        /// <summary>
        /// Gets an entity by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Counts the total number of entities in the repository asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The total number of entities.</returns>
        Task<int> CountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists a range of entities by their identifiers asynchronously.
        /// </summary>
        /// <param name="ids">The identifiers of the entities.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The list of entities.</returns>
        Task<IEnumerable<T>> ListRangeAsync(IEnumerable<object> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists all entities in the repository asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The list of all entities.</returns>
        Task<IEnumerable<T>> ListAllAsync(CancellationToken cancellationToken = default);
    }
}