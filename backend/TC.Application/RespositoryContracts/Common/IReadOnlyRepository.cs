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
        /// <remarks> This method is intended to be used when retrieving an entity by its unique identifier. It can be used to fetch specific records from the repository, enabling users to access detailed information about a particular entity. </remarks>
        Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Counts the total number of entities in the repository asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The total number of entities.</returns>
        /// <remarks> This method is intended to be used when counting the total number of entities in the repository. It can be used to get an overview of the data size, facilitating management and reporting tasks. </remarks>
        Task<int> CountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists a range of entities by their identifiers asynchronously.
        /// </summary>
        /// <param name="ids">The identifiers of the entities.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The list of entities.</returns>
        /// <remarks> This method is intended to be used when retrieving a range of entities by their unique identifiers. It can be used to fetch multiple specific records from the repository, enabling users to access detailed information about a set of entities. </remarks>
        Task<IEnumerable<T>> ListRangeAsync(IEnumerable<object> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists all entities in the repository asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The list of all entities.</returns>
        /// <remarks> This method is intended to be used when retrieving all entities in the repository. It can be used to fetch a complete list of records, enabling users to access detailed information about all entities in the data source. </remarks>
        Task<IEnumerable<T>> ListAllAsync(CancellationToken cancellationToken = default);
    }
}