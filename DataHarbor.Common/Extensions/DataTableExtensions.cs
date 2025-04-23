using System.Data;

namespace DataHarbor.Common.Extensions
{
    public static class DataTableExtensions
    {
        public static DataTable EnsureUniqueEntries(this DataTable sourceTable, DataTable tableToMerge, string propertyName)
        {
            // Find new transaction ids.
            var existingIds = tableToMerge.AsEnumerable()
                .Select(x => x.Field<string>(propertyName)).Distinct();

            // remove existing entries if exists.
            var uniqueRows = sourceTable.AsEnumerable()
                .Where(row => !existingIds.Contains(row.Field<string>(propertyName)));

            if (uniqueRows.Count() > 0)
            {
                DataTable updated = uniqueRows.CopyToDataTable();
                updated.Merge(tableToMerge);
                updated.AcceptChanges();
                return updated;
            }
            return tableToMerge;
        }
    }
}
