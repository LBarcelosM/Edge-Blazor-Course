using Microsoft.EntityFrameworkCore;

namespace Edge.Bills.Data.Repositories
{
    public class BaseEntityRepositoryClass<TEntity, TKey>
        where TEntity : class
    {
        private readonly DbContext _dbContext;

        public BaseEntityRepositoryClass(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddEntity(TEntity entity)
        {
            await GetSet().AddAsync(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(TKey id)
        {
            var entity = await GetEntityById(id);
            if(entity != null)
            {
                GetSet().Remove(entity);
                return await _dbContext.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<TEntity?> GetEntityById(TKey id)
        {
            return await _dbContext.FindAsync<TEntity>(id);
        }


        public async Task<IEnumerable<TEntity>> GetQueryable(params string[] include)
        {
            var query = GetSet().AsQueryable();
            if(include != null && include.Any())
            {
                foreach(var includeValue in include)
                {
                    query = query.Include(includeValue);
                }
            }
            return query;
        }

        public async Task<int> UpdateEntity(TEntity entity)
        {
            GetSet().Update(entity);
            return await _dbContext.SaveChangesAsync();
        }

        protected DbSet<TEntity> GetSet() => _dbContext.Set<TEntity>();
    }
}
