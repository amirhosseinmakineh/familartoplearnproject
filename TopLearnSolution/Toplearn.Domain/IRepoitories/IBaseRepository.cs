using Toplearn.Domain.Models;

namespace Toplearn.InfraStructure.IRepoitories
{
    public interface IBaseRepository<Tentity,Tkey> where Tentity : BaseEntity<Tkey> where Tkey : struct
    {
        IQueryable<Tentity> GetAll();
        int SaveChanges();
        void Add(Tentity entity);
        void Delet(Tentity tentity);
        Tentity GetById(Tkey id);
    }
}
