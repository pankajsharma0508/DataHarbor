using DataHarbor.Extractors.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHarbor.Extractors.Processors
{
    internal class DatFileProcessor : IFileProcessor
    {
        public bool CanProcess(string fileExtension) => fileExtension.Equals(FileExtensions.DAT);

        public void ProcessFile(string filePath)
        {
        }
    }
}
