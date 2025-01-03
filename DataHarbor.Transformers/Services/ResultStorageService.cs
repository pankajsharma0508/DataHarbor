using DataHarbor.Common.Models;
using DataHarbor.Repository;
using System.Threading.Tasks.Dataflow;

namespace DataHarbor.Transformers.Services
{
    public class ResultStorageService
    {
        private readonly IRepository<ProcessResult> _processResultRepository;

        public ResultStorageService(IRepository<ProcessResult> processResultRepository)
        {
            _processResultRepository = processResultRepository;
        }

        public ActionBlock<ProcessResult> MainBlock => new(StoreResults);
        async void StoreResults(ProcessResult request)
        {
            await _processResultRepository.Add(request);
        }
    }
}
