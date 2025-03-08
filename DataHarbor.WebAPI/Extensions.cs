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
    }
}
