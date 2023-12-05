using Nebula.Domain.Commons;
using System.Linq.Expressions;

namespace Nebula.Application.Interfaces;

public interface IRepository<T> where T : Auditable
{
    Task InsertAsync(T entity);
    void Update(T entity);
    void Drop(T entity);
    Task<T> SelectAsync(Expression<Func<T, bool>> expression = null!, string[] includes = null!);
    Task<T> GetEntityWithRelatedAsync(long entityId, params Expression<Func<T, object>>[] includes);
    IQueryable<T> SelectAll(Expression<Func<T, bool>> expression = null!,bool isNoTracking = true, string[] includes = null!);
    Task SaveAsync();
}
