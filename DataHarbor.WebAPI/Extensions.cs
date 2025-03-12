using System.Data;

namespace DataHarbor.WebAPI
{
    public static class Extensions
    {
        public static List<Dictionary<string, object>> ToDictionaryList(this DataTable table)
        {
            return table.AsEnumerable().Select(row =>
                table.Columns.Cast<DataColumn>().ToDictionary(
                    column => column.ColumnName,
                    column => row[column] is DBNull ? null : row[column]
                )
            ).ToList();
        }

        public static DataTable ToDataTable(this List<Dictionary<string, string>> entries)
        {
            var table = new DataTable();
            if (entries.Count > 0)
            {
                var columnNames = entries.First().Keys;
                foreach (var columnName in columnNames.Distinct())
                {
                    table.Columns.Add(new DataColumn(columnName));
                }

                foreach (var transactions in entries)
                {
                    var newRow = table.NewRow();
                    foreach (var keyValue in transactions)
                    {
                        newRow[keyValue.Key] = keyValue.Value;
                    }
                    table.Rows.Add(newRow);
                }
                table.AcceptChanges();
            }
            return table;
        }
    }
}
