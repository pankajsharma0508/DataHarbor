using DataHarbor.Common.Repository;

namespace DataHarbor.Loaders.Services
{
    public interface IDbaseService<T> where T : class
    {
        bool CreateOrUpdateFile(string filePath, IEnumerable<T> entries);
    }
}
