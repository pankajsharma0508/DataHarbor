using DataHarbor.Common.Models;
using DataHarbor.Transformers.Helpers;
using System.Dynamic;
using System.Threading.Tasks.Dataflow;

namespace DataHarbor.Transformers.Services
{
    public class MappingService
    {
        //public static TransformBlock<ProcessRequest, ProcessResult> MainBlock => new(MapData);
        //public static ProcessResult MapData(ProcessRequest request)
        //{
        //    //var result = new ProcessResult
        //    //{
        //    //    Id = Guid.NewGuid().ToString(),
        //    //    UniqueId = new Guid(request.Id),
        //    //    RecieveDate = request.RecieveDate,
        //    //    Name = request.Name,
        //    //    Description = request.Description
        //    //};
        //    //var mappings = GetPropertyMapping();
        //    //var index = 0;
        //    //foreach (var entry in request.Data.Rows)
        //    //{
        //    //    if (entry != request.Data.Rows.First())
        //    //    {
        //    //        var transaction = MappingHelper.MapProperties<Transaction>(entry, mappings);
        //    //        transaction.Id = ++index;
        //    //        result.Entries.Add(transaction);
        //    //    }
        //    //}
        //    return result;
        //}
        public static Dictionary<string, string> GetPropertyMapping()
        {
            var mapping = new Dictionary<string, string>();
            mapping.Add("column1", "Description");
            mapping.Add("column2", "TransactionCode");
            mapping.Add("column3", "TransactionDate");
            mapping.Add("column4", "BatchId");
            mapping.Add("column5", "ItemId");
            mapping.Add("column6", "NumberOfItems");
            return mapping;
        }
    }
}
