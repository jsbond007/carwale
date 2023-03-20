using Carwale.Domain.Entities;

namespace Carwale.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> Get<T>(T id);
        Task<Tuple<string, int>> Create(TEntity entity, bool saveChanges = true);

        Task Update<TModifiableEntity>(TModifiableEntity entity, bool saveChanges = true) where TModifiableEntity : BaseEntity, IModifiableEntity;

        Task Delete<T>(T id, bool saveChanges = true);

    }
}
