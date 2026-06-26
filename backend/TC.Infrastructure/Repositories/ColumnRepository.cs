using TC.Application.RespositoryContracts;
using TC.Domain.Entities;
using TC.Infrastructure.DBContext;

namespace TC.Infrastructure.Repositories
{
    public class ColumnRepository : BaseRepository<Column, int>, IColumnRepository
    {
        private readonly TrelloCloneDBContext _context;

        public ColumnRepository(TrelloCloneDBContext context) : base(context)
        {
            _context = context;
        }
    }
}