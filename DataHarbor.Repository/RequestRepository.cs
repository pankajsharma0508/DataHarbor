using DataHarbor.Common.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace DataHarbor.Repository
{
    public class RequestRepository : IRepository<ProcessRequest>
    {
        public async Task<bool> Add(ProcessRequest request)
        {
            using (var session = DocumentDBContext.DocumentStore.OpenAsyncSession())
            {
                await session.StoreAsync(request);
                await session.SaveChangesAsync();
            }
            return true;
        }

        public async Task<ProcessRequest> GetByID(string id)
        {
            using var session = DocumentDBContext.DocumentStore.OpenAsyncSession();
            return await session.LoadAsync<ProcessRequest>(id);
        }

        public async Task<List<ProcessRequest>> GetAll()
        {
            using var session = DocumentDBContext.DocumentStore.OpenAsyncSession();
            return await session.Query<ProcessRequest>().ToListAsync();
        }

        public async Task Delete(string id)
        {
            using (var session = DocumentDBContext.DocumentStore.OpenAsyncSession())
            {
                var user = await session.LoadAsync<ProcessRequest>(id);
                if (user != null)
                {
                    session.Delete(user);
                    await session.SaveChangesAsync();
                }
            }
        }
    }

    public interface IRepository<T>
    {
        Task<bool> Add(T request);
        Task<T> GetByID(string id);
        Task Delete(string id);
        Task<List<T>> GetAll();
    }
}


