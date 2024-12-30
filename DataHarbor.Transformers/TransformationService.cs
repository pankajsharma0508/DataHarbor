using DataHarbor.Common.Models;
using DataHarbor.Repository;
using DataHarbor.Transformers.Services;
using System.Threading.Tasks.Dataflow;

namespace DataHarbor.Transformers
{
    public class TransformationService : ITransformationService
    {
        public IRepository<ProcessRequest> _requestRepository { get; set; }

        public TransformationService(IRepository<ProcessRequest> repository)
        {
            _requestRepository = repository;
        }

        public void Transform()
        {
            // Read Raw Data
            var rawData = _requestRepository.GetAll().Result.First();
            
            // Validate Data

            // Map Data
            var mainBlock = MappingService.MainBlock;
            var processResult = mainBlock.Post(rawData);
        }
    }
}
