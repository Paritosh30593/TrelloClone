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
    }
}