using DataHarbor.Common.Configuration;
using DataHarbor.Common.Models;
using DataHarbor.Repository;
using DataHarbor.Transformers.Interfaces;

namespace DataHarbor.Transformers.Services
{
    public class StorageService: IStorageService
    {
        public IRepository<ProcessRequest> _requestRepository;
        public IRepository<ProcessResult> _resultRepository;
        public IRepository<ProcessingConfiguration> _configurationRepository;


        public StorageService(IRepository<ProcessResult> resultRepository, IRepository<ProcessRequest> requestRepository)
        {
            _resultRepository = resultRepository;
            _requestRepository = requestRepository;
        }

        public Task<ProcessRequest> GetRequestByID(Guid id) => _requestRepository.GetByID(id.ToString());

        public Task<bool> SaveResults(ProcessResult result) => _resultRepository.Add(result);

        public Task<ProcessingConfiguration> GetConfiguration(string name) => _configurationRepository.FirstOrDefault(x=> x.Name == name);

    }
}
