using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TC.Application.RespositoryContracts.Common;
using TC.Domain.Entities;

namespace TC.Application.RespositoryContracts
{
    public interface ICardRepository : IBaseRepository<Card>
    {
        #region Getters
        /// <summary>
        /// Gets all cards asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A collection of all cards.</returns>
        /// <remarks> This method is intended to be used when retrieving all cards in the system. </remarks>
        Task<IEnumerable<Card>> GetAllCardsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all cards by column ID asynchronously.
        /// </summary>
        /// <param name="columnId">The ID of the column.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A collection of cards in the specified column.</returns>
        /// <remarks> This method is intended to be used when retrieving all cards associated with a specific column. </remarks>
        Task<IEnumerable<Card>> GetCardsByColumnIdAsync(int columnId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a card by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the card.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The card with the specified ID.</returns>
        /// <remarks> This method is intended to be used when retrieving a card by its ID. </remarks>
        Task<Card> GetCardByIdAsync(int id, CancellationToken cancellationToken = default);
        #endregion


        #region Adders
        /// <summary>
        /// Adds a new card asynchronously.
        /// </summary>
        /// <param name="card">The card to add.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The added card.</returns>
        /// <remarks> This method is intended to be used when creating a new card. </remarks>
        Task<Card> AddCardAsync(Card card, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a list of default cards to a column asynchronously.
        /// </summary>
        /// <param name="cards">The list of cards to add.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The number of state entries written to the database.</returns>
        /// <remarks> This method is intended to be used when adding default cards to a newly created column. </remarks>
        Task<int> AddDefaultCardsToColumnAsync(List<Card> cards, CancellationToken cancellationToken = default);
        #endregion


        #region Updaters
        /// <summary>
        /// Updates an existing card asynchronously.
        /// </summary>
        /// <param name="card">The card to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The updated card.</returns>
        /// <remarks> This method is intended to be used when updating an existing card. </remarks>
        Task<Card> UpdateCardAsync(Card card, CancellationToken cancellationToken = default);
        #endregion


        #region Deleters
        /// <summary>
        /// Deletes a card by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the card to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>    
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        /// <remarks> This method is intended to be used when deleting a card by its ID. </remarks>
        Task<bool> DeleteCardAsync(int id, CancellationToken cancellationToken = default);
        #endregion
    }
}