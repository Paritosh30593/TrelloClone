using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TC.Application.RespositoryContracts;
using TC.Domain.Entities;
using TC.Infrastructure.DBContext;

namespace TC.Infrastructure.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        private readonly TrelloCloneDBContext _context;

        public CardRepository(TrelloCloneDBContext context) : base(context)
        {
            _context = context;
        }

        #region Getters
        public async Task<IEnumerable<Card>> GetAllCardsAsync(CancellationToken cancellationToken = default)
        {
            return await ListAllAsync(cancellationToken);
        }

        public async Task<IEnumerable<Card>> GetCardsByColumnIdAsync(int columnId, CancellationToken cancellationToken = default)
        {
            return await _context.Card
                .Where(c => c.ColumnId == columnId)
                .OrderBy(c => c.SortOrder)
                .ToListAsync(cancellationToken);
        }

        public async Task<Card> GetCardByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetByIdAsync(id, cancellationToken);
        }
        #endregion


        #region Adders
        public async Task<Card> AddCardAsync(Card card, CancellationToken cancellationToken = default)
        {
            return await CreateAsync(card, cancellationToken);
        }

        public async Task<IEnumerable<Card>> AddDefaultCardsToColumnAsync(List<Card> cards, CancellationToken cancellationToken = default)
        {
            await _context.Card.AddRangeAsync(cards, cancellationToken);

            return await _context.SaveChangesAsync(cancellationToken) > 0
                ? cards
                : new List<Card>();
        }
        #endregion


        #region Updaters
        public async Task<Card> UpdateCardAsync(Card card, CancellationToken cancellationToken = default)
        {
            return await UpdateAsync(card, cancellationToken);
        }

        public async Task<IEnumerable<Card>> UpdateCardsBulkAsync(IEnumerable<Card> cards, CancellationToken cancellationToken = default)
        {
            return await UpdateRangeAsync(cards, cancellationToken);
        }
        #endregion


        #region Deleters
        public async Task<bool> DeleteCardAsync(int id, CancellationToken cancellationToken = default)
        {
            Card cardToDelete = await GetByIdAsync(id, cancellationToken);

            return cardToDelete != null && await DeleteAsync(cardToDelete, cancellationToken);
        }
        #endregion
    }
}