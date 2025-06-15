using System.Linq.Expressions;
using DocumentApprovalSystemTask.Domain.Entities;

namespace DocumentApprovalSystemTask.Domain.Interfaces
{
    public interface IGeneralRepository<T> where T : BaseModel
    {
        void Add(T obj);
        void Update(T obj);
        void Remove(int id);
        T GetByID(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        Task SaveChangesAsync();
    }
}
