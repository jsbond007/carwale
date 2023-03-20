using Carwale.Domain;
using Carwale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Carwale.Domain.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {

        public readonly CwDbContext _dbContext;

        public BaseRepository(CwDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IQueryable<TEntity> GetAll()
        {
            var query = _dbContext.Set<TEntity>();
            return query;
        }
        public IQueryable<TEntity> GetReadOnlyList()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }
        public async Task<TEntity> Get<T>(T id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<Tuple<string, int>> Create(TEntity entity, bool saveChanges = true)
        {
            entity.UId = UIdGenerator.GenerateUId();
            entity.CreatedDateTime = DateTime.UtcNow;
            var result = await _dbContext.Set<TEntity>().AddAsync(entity);

            if (saveChanges)
            {
                await Save();
                return new Tuple<string, int>(result.Entity.UId, result.Entity.Id);
            }
            else
            {
                return new Tuple<string, int>("", 0);
            }
        }


        public async Task Update<TModifiableEntity>(TModifiableEntity entity, bool saveChanges = true) where TModifiableEntity : BaseEntity, IModifiableEntity
        {
            entity.ModifiedDateTime = DateTime.UtcNow;
            _dbContext.Set<TModifiableEntity>().Update(entity);
            if (saveChanges)
            {
                await Save();
            }
        }

        public async Task Delete<T>(T id, bool saveChanges = true)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);

            if (entity != null)
            {
                _dbContext.Set<TEntity>().Remove(entity);
                if (saveChanges)
                {
                    await Save();
                }
            }
        }

        public virtual async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
