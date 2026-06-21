using TC.Domain.Common;

namespace TC.Domain.RespositoryContracts
{
    public interface IBaseRepository<T, K> where T : BaseEntity<K>
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T?> Get(K id, CancellationToken cancellationToken);
        Task<List<T>> GetAll(CancellationToken cancellationToken);
    }
}