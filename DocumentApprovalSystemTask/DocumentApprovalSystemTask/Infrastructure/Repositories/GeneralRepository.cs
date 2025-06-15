using System.Linq.Expressions;
using DocumentApprovalSystemTask.Domain.Entities;
using DocumentApprovalSystemTask.Domain.Interfaces;
using DocumentApprovalSystemTask.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DocumentApprovalSystemTask.Infrastructure.Repositories
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : BaseModel
    {
        ApplicationDbContext _context;
        DbSet<T> _dbSet;

        public GeneralRepository(ApplicationDbContext _context)
        {
            this._context = _context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T obj)
        {
            _context.Set<T>().Add(obj);
        }

        public void Update(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Remove(int id)
        {
            T t = Get(x => x.Id == id).FirstOrDefault();
            _dbSet.Remove(t);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public T GetByID(int id)
        {
            return Get(x => x.Id == id).FirstOrDefault();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
