using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WashMyCar.Core.DataContracts;
using WashMyCar.Core.Domain;

namespace WashMyCar.DataAccess.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        protected readonly DbSet<TEntity> DbSet;

        protected Repository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual async Task<bool> Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            return await SaveChanges() > 0;
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<bool> Update(TEntity entity)
        {
            DbSet.Update(entity);
            return await SaveChanges() > 0;
        }

        public virtual async Task<bool> Remove(TEntity entity)
        {
            DbSet.Remove(entity);
            return await SaveChanges() > 0;
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}