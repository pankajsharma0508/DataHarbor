using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHarbor.Common.Helpers
{
    public static class DataTableHelper
    {
        public static void AssureColumns(this DataTable table, List<string> columns)
        {
            columns.ForEach(x => table.AssureColumn(x));
        }

        public static void AssureColumn(this DataTable table, string column)
        {
            if (!table.Columns.Contains(column))
            {
                table.Columns.Add(column);
            }
        }
    }
}
