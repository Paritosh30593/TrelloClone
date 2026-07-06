using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TC.Application.RespositoryContracts.Common
{
    public interface IBaseRepository<T> : IReadOnlyRepository<T> where T : class
    {
        /// <summary>
        /// Creates a new entity in the repository and returns the created entity.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The created entity.</returns>
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);


        /// <summary>
        /// Updates an existing entity in the repository and returns a boolean indicating whether the update was successful.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A boolean indicating whether the update was successful.</returns>
        Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);


        /// <summary>
        /// Deletes an existing entity in the repository and returns a boolean indicating whether the deletion was successful.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default);


        /// <summary>
        /// Deletes a range of entities in the repository and returns a boolean indicating whether the deletion was successful.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    }
}