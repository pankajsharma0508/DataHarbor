using DataHarbor.Common.Models;
using DataHarbor.Repository;
using Raven.Client.Documents;
using System.Linq.Expressions;
using System;

namespace DataHarbor.Common.Repository
{
    public class DocumentRepository<T> : IRepository<T> where T : BaseDocument
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
            var results = await session.Query<T>().ToListAsync();

            return results;
        }

        public async Task<T> GetByID(string id)
        {
            using var session = DocumentDBContext.DocumentStore.OpenAsyncSession();
            return await session.LoadAsync<T>(id);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            using var session = DocumentDBContext.DocumentStore.OpenAsyncSession();
            return await session.Query<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> Where(Expression<Func<T, bool>> predicate)
        {
            using var session = DocumentDBContext.DocumentStore.OpenAsyncSession();
            return await session.Query<T>().Where(predicate).ToListAsync();
        }
    }
}
