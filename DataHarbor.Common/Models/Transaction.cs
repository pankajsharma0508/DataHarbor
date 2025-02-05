namespace DataHarbor.Common.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string BatchId { get; set; }
        public string ItemId
        {
            get; set;
        }
        public string TransactionCode { get; set; }
        public int NumberOfItems { get; set; }
        public DateTime TrasactionDate { get; set; }
        public string? CorrectionCode { get; set; }
        public DateTime? CorrectionDate { get; set; }
    }
}
