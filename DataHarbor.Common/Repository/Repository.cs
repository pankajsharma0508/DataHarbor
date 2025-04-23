using DataHarbor.Common.Models;
using DataHarbor.Repository;
using Raven.Client.Documents;
using System.Linq.Expressions;
using System;
using System.Xml;
using static System.Collections.Specialized.BitVector32;

namespace DataHarbor.Common.Repository
{
    public class DocumentRepository<T> : IRepository<T> where T : IDocument
    {
        public async Task<T> Add(T document)
        {
            using (var session = DocumentDBContext.DocumentStore.OpenAsyncSession())
            {
                await session.StoreAsync(document);
                await session.SaveChangesAsync();

                return await session.LoadAsync<T>(document.Id);
            }
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

        public async Task<T> Update(T document)
        {
            using (var session = DocumentDBContext.DocumentStore.OpenAsyncSession())
            {
                var entity = await session.LoadAsync<T>(document?.Id);
                if (entity == null)
                    throw new Exception($"Unable to find {nameof(T)}, with id: {document?.Id}");

                DocumentRepository<T>.CopyProperties(document, entity);
                await session.SaveChangesAsync();

                return await session.LoadAsync<T>(document.Id);
            }
        }

        private static void CopyProperties(T source, T destination)
        {
            if (source == null || destination == null)
                throw new ArgumentNullException("Source or Destination cannot be null");

            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                if (prop.CanRead && prop.CanWrite)
                {
                    var value = prop.GetValue(source);
                    prop.SetValue(destination, value);
                }
            }
        }
    }
}
