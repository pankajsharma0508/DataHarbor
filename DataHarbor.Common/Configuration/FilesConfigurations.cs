using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHarbor.Common.Configuration
{
    public class FilesConfigurations
    {
        public string FileCategory { get; set; }
        public string FileFormat { get; set; }
        public string LineSeparator { get; set; }
        public string ColumnSeparator { get; set; }
        public string TextQualifier { get; set; }
        public bool FirstRowHasHeaders { get; set; }
        public int HeaderRowsToIgnore { get; set; }
        public int FooterRowsToIgnore { get; set; }
        public string DecimalSeparator { get; set; }
        public string DigitalGroupSeparator { get; set; }

    }
}
