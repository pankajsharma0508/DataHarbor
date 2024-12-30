namespace DataHarbor.Repository
{
    public interface IRepository<T>
    {
        Task<bool> Add(T request);
        Task<T> GetByID(string id);
        Task Delete(string id);
        Task<List<T>> GetAll();
    }
}
