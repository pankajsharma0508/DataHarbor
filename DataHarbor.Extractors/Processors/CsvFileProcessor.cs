using DataHarbor.Extractors.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHarbor.Extractors.Processors
{
    public class CsvFileProcessor : IFileProcessor
    {
        public bool CanProcess(string fileExtension) => fileExtension.Equals(FileExtensions.CSV);
       
        public void ProcessFile(string filePath)
        {
        }
    }
}
