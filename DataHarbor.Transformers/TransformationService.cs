using DataHarbor.Common.Models;
using DataHarbor.Repository;
using DataHarbor.Transformers.Services;
using System.Threading.Tasks.Dataflow;

namespace DataHarbor.Transformers
{
    public class TransformationService : ITransformationService
    {
        public IRepository<ProcessRequest> _requestRepository;
        public IRepository<ProcessResult> _resultRepository;


        public TransformationService(IRepository<ProcessRequest> repository, IRepository<ProcessResult> resultRepository)
        {
            _requestRepository = repository;
            _resultRepository = resultRepository;
        }

        public async Task Transform(string id)
        {
            // Read Raw Data
            var rawData = await _requestRepository.GetByID(id);
            
            // Validate Data

            // Map Data
            var mainBlock = MappingService.MainBlock;
            
            var storageBlock = new ResultStorageService(_resultRepository).MainBlock;
            mainBlock.LinkTo(storageBlock);

            mainBlock.Post(rawData);
        }
    }
}
