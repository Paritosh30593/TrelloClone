using TC.Application.RespositoryContracts;
using TC.Domain.Entities;
using TC.Infrastructure.DBContext;

namespace TC.Infrastructure.Repositories
{
    public class BoardRepository : BaseRepository<Board, int>, IBoardRepository
    {
        private readonly TrelloCloneDBContext _context;

        public BoardRepository(TrelloCloneDBContext context) : base(context)
        {
            _context = context;
        }
    }
}