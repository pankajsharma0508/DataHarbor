namespace DataHarbor.Common.Models
{
    public class Attachment
    {
        public string FileName { get; set; }

        public Stream FileStream { get; set; }

        public string FileContentType { get; set; }
    }
}
