using Toplearn.Domain.Models;
using Toplearn.InfraStructure.Context;
using Toplearn.InfraStructure.IRepoitories;

namespace Toplearn.Domain.Repositories
{
    public class BaseRepository<TEntity, Tkey> : IBaseRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey> where Tkey : struct
    {
        private readonly TopleaarnContext context;

        public BaseRepository(TopleaarnContext context)
        {
            this.context = context;
        }

        public void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public void Delet(TEntity tentity)
        {
            var entity = GetById(tentity.Id);
            entity.IsDelet = true;
        }

        public IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }

        public TEntity GetById(Tkey id)
        {
            var entity = context.Set<TEntity>().Find(id);
            return entity;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
