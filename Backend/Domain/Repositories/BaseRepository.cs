using Carwale.Domain;
using Carwale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Carwale.Domain.Repositories
{
    /// <summary>
    /// Base Repository has all the base functions to be used 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {

        public readonly CwDbContext _dbContext;

        public BaseRepository(CwDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get all entities as Queryable
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAll()
        {
            var query = _dbContext.Set<TEntity>();
            return query;
        }

        /// <summary>
        /// Get All Entities as Querable without Tracking so updating any entity from this Query will not be updated
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetReadOnlyList()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }
        public virtual async Task<TEntity> Get<T>(T id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// This will insert a new entity in the database if SaveChanges is True
        /// It will create a new Unique Random UId 
        /// IT will put CreatedDateTime as DateTime.UtcNow
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <returns>This will return the newly created entity Id and UId as Tuple</returns>
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

        /// <summary>
        /// This will update the entity if Entity is Updatable, inherits IModifiableEntity interface
        /// On update ModifiedDateTime is set to DateTime.Now.UtcNow
        /// </summary>
        /// <typeparam name="TModifiableEntity"></typeparam>
        /// <param name="entity">Entity to be updated, must be attached to the set</param>
        /// <param name="saveChanges">If Save changes is true only it will update to database</param>
        /// <returns></returns>
        public virtual async Task Update<TModifiableEntity>(TModifiableEntity entity, bool saveChanges = true) where TModifiableEntity : BaseEntity, IModifiableEntity
        {
            entity.ModifiedDateTime = DateTime.UtcNow;
            _dbContext.Set<TModifiableEntity>().Update(entity);
            if (saveChanges)
            {
                await Save();
            }
        }

		/// <summary>
		/// This will delete the Entity for given UId or Id
		/// </summary>
		/// <typeparam name="T">Id or UId</typeparam>
		/// <param name="id">Id or UId</param>
		/// <param name="saveChanges">If Save changes is true only it will update to database</param>
		/// <returns></returns>
		public virtual async Task Delete<T>(T id, bool saveChanges = true)
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

        /// <summary>
        /// Updated the pending changes to database
        /// </summary>
        /// <returns></returns>
        public virtual async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
