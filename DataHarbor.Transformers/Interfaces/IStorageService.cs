using DataHarbor.Common.Configuration;
using DataHarbor.Common.Models;

namespace DataHarbor.Transformers.Interfaces
{
    public interface IStorageService
    {
        public Task<ProcessRequest> GetRequestByID(Guid id);

        public Task SaveResults(ProcessRequest result);

        Task<ProcessingConfiguration> GetConfiguration(string name);
    }
}
