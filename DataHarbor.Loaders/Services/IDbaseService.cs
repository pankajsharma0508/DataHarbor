using DataHarbor.Common.Repository;
using System.Data;

namespace DataHarbor.Loaders.Services
{
    public interface IDbaseService<T> where T : class
    {
        bool CreateOrUpdateFile(string filePath, DataTable dataTable);
    }
}
