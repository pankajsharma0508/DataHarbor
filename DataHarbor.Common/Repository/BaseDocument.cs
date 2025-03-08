namespace DataHarbor.Common.Repository
{
    public class BaseDocument(string id)
    {
        public string Id { get; set; } = id;
    }
}
