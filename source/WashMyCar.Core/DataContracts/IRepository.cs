using System.Linq.Expressions;
using WashMyCar.Core.Domain;

namespace WashMyCar.Core.DataContracts
{
    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task<bool> Add(TEntity entity);
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<bool> Update(TEntity entity);
        Task<bool> Remove(TEntity entity);
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}