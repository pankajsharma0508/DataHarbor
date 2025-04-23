using System.Linq.Expressions;

namespace DataHarbor.Repository
{
    public interface IRepository<T>
    {
        Task<T> Add(T request);
        Task<T> GetByID(string id);
        Task<List<T>> Where(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task Delete(string id);
        Task<List<T>> GetAll();
        Task<T> Update(T document);
    }
}
