using DataHarbor.Common.Models;
using DataHarbor.Common.Repository;
using dBASE.NET.Core;
using System;
using System.Data;
using System.Reflection;

namespace DataHarbor.Loaders.Services
{
    public class DbaseService<T> : IDbaseService<T> where T : class
    {
        public bool IsFileExists(string filePath) => File.Exists(filePath);

        private bool UpdateFile(string filePath, DataTable dataTable)
        {
            var dbf = new Dbf();
            dbf.Read(filePath);
            AddRecordsToDbf(filePath, dataTable, dbf);
            return true;
        }

        private bool CreateFile(string filePath, DataTable dataTable)
        {
            var dbf = new Dbf();
            foreach (DataColumn column in dataTable.Columns)
            {
                DbfField field = new DbfField(column.ColumnName, DbfFieldType.Character, Byte.MaxValue);
                dbf.Fields.Add(field);
            }
            AddRecordsToDbf(filePath, dataTable, dbf);

            return true;
        }

        private void AddRecordsToDbf(string filePath, DataTable dataTable, Dbf dbf)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                DbfRecord record = dbf.CreateRecord();
                var columnCount = 0;
                foreach (var dbfField in dbf.Fields)
                {
                    record.Data[columnCount] = row[dbfField.Name];
                    columnCount++;
                }
            }
            dbf.Write(filePath, DbfVersion.FoxBaseDBase3NoMemo);
        }

        public bool CreateOrUpdateFile(string filePath, DataTable dataTable)
        {
            var properties = typeof(T).GetProperties();
            return !File.Exists(filePath) ? CreateFile(filePath, dataTable) : UpdateFile(filePath, dataTable);
        }
    }
}
