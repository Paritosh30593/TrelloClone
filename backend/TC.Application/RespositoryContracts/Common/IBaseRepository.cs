using System.Collections.Generic;
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
        /// <remarks> This method is intended to be used when creating a new entity in the repository. It can be used to add entities with specific properties, enabling users to create new records for their data. </remarks>
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);


        /// <summary>
        /// Creates a range of new entities in the repository and returns the created entities.
        /// </summary>
        /// <param name="entities">The entities to create.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The created entities.</returns>
        /// <remarks> This method is intended to be used when creating a range of new entities in the repository. It can be used to add multiple entities with specific properties, enabling users to create new records for their data in bulk. </remarks>
        Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);


        /// <summary>
        /// Updates an existing entity in the repository and returns the updated entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The updated entity.</returns>
        /// <remarks> This method is intended to be used when updating an existing entity in the repository. It can be used to modify entity details such as name, description, or other relevant information, facilitating the management of records over time. </remarks>
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);


        /// <summary>
        /// Updates a range of existing entities in the repository and returns the updated entities.
        /// </summary>
        /// <param name="entities">The entities to update.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The updated entities.</returns>
        /// <remarks> This method is intended to be used when updating a range of existing entities in the repository. It can be used to modify multiple entity details such as name, description, or other relevant information, facilitating the management of records over time. </remarks>
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);


        /// <summary>
        /// Deletes an existing entity in the repository and returns a boolean indicating whether the deletion was successful.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        /// <remarks> This method is intended to be used when deleting an existing entity in the repository. It can be used to remove records that are no longer needed, helping to maintain a clean and organized set of data. </remarks>
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default);


        /// <summary>
        /// Deletes a range of entities in the repository and returns a boolean indicating whether the deletion was successful.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        /// <remarks> This method is intended to be used when deleting a range of existing entities in the repository. It can be used to remove multiple records that are no longer needed, helping to maintain a clean and organized set of data. </remarks>
        Task<bool> DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    }
}