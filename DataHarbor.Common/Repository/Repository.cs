using DataHarbor.Common.Models;
using DataHarbor.Repository;
using Raven.Client.Documents;

namespace DataHarbor.Common.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseDocument
    {
        public async Task<bool> Add(T request)
        {
            try
            {
                using (var session = DocumentDBContext.DocumentStore.OpenAsyncSession())
                {
                    await session.StoreAsync(request);
                    await session.SaveChangesAsync();
                }

            }
            catch (Exception) { return false; }
            return true;
        }

        public async Task Delete(string id)
        {
            using (var session = DocumentDBContext.DocumentStore.OpenAsyncSession())
            {
                var user = await session.LoadAsync<ProcessResult>(id);
                if (user != null)
                {
                    session.Delete(user);
                    await session.SaveChangesAsync();
                }
            }
        }

        public async Task<List<T>> GetAll()
        {
            using var session = DocumentDBContext.DocumentStore.OpenAsyncSession();
            return await session.Query<T>().ToListAsync();
        }

        public async Task<T> GetByID(string id)
        {
            using var session = DocumentDBContext.DocumentStore.OpenAsyncSession();
            return await session.LoadAsync<T>(id);
        }
    }
}
