using DataHarbor.Common.Models;
using DataHarbor.Repository;
using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHarbor.Common.Repository
{
    public class ProcessResultRepository : IRepository<ProcessResult>
    {
        public async Task<bool> Add(ProcessResult request)
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

        public async Task<List<ProcessResult>> GetAll()
        {
            using var session = DocumentDBContext.DocumentStore.OpenAsyncSession();
            return await session.Query<ProcessResult>().ToListAsync();
        }

        public async Task<ProcessResult> GetByID(string id)
        {
            using var session = DocumentDBContext.DocumentStore.OpenAsyncSession();
            return await session.LoadAsync<ProcessResult>(id);
        }
    }
}
