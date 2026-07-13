using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TC.Application.DTO.CardDTO;

namespace TC.Application.ServiceContracts
{
    public interface ICardService
    {
        #region Getters
        /// <summary>
        /// Gets all cards asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A list of <see cref="CardResponse"/> objects.</returns>
        /// <remarks> This method is intended to be used when retrieving all cards in the system. </remarks>
        Task<List<CardResponse>> GetAllCardsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a card by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the card.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="CardResponse"/> object.</returns>
        /// <remarks> This method is intended to be used when retrieving a specific card by its unique identifier. </remarks>
        Task<CardResponse> GetCardByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all cards for a specific column asynchronously.
        /// </summary>
        /// <param name="columnId">The ID of the column.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A list of <see cref="CardResponse"/> objects.</returns>
        /// <remarks> This method is intended to be used when retrieving all cards associated with a specific column. </remarks>
        Task<List<CardResponse>> GetCardsByColumnIdAsync(int columnId, CancellationToken cancellationToken = default);
        #endregion


        #region Adders
        /// <summary>
        /// Adds a new card asynchronously.
        /// </summary>
        /// <param name="cardRequest">The card request object.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="CardResponse"/> object.</returns>
        /// <remarks> This method is intended to be used when a new card is created, and it will add the card to the specified column. </remarks>
        Task<CardResponse> AddCardAsync(CardAddRequest cardRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds default cards for a specific column asynchronously.
        /// </summary>
        /// <param name="columnId">The ID of the column.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A list of <see cref="CardResponse"/> objects.</returns>
        /// <remarks> This method is intended to be used when a new column is created, and it will add default cards to that column. </remarks>
        Task<int> AddDefaultCardsToColumnAsync(int columnId, CancellationToken cancellationToken = default);
        #endregion


        #region Updaters
        /// <summary>
        /// Updates an existing card asynchronously.
        /// </summary>
        /// <param name="cardRequest">The card request object.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="CardResponse"/> object.</returns>
        /// <remarks> This method is intended to be used when an existing card needs to be updated with new information. </remarks>
        Task<CardResponse> UpdateCardAsync(CardUpdateRequest cardRequest, CancellationToken cancellationToken = default);
        #endregion


        #region Deleters
        /// <summary>
        /// Deletes a card by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the card.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A <see cref="CardResponse"/> object.</returns>
        /// <remarks> This method is intended to be used when an existing card needs to be removed from the system. </remarks>
        Task<bool> DeleteCardAsync(int id, CancellationToken cancellationToken = default);
        #endregion
    }
}