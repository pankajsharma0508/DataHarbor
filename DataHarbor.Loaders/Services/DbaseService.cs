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

        private bool UpdateFile(string filePath, IEnumerable<T> entries, IEnumerable<PropertyInfo> properties)
        {
            var dbf = new Dbf();
            dbf.Read(filePath);
            AddRecordsToDbf(filePath, entries, dbf, properties);
            return true;
        }

        private bool CreateFile(string filePath, IEnumerable<T> entries, IEnumerable<PropertyInfo> properties)
        {
            var dbf = new Dbf();
            foreach (PropertyInfo property in properties)
            {
                DbfField field = new DbfField(property.Name, DbfFieldType.Character, Byte.MaxValue);
                dbf.Fields.Add(field);
            }
            AddRecordsToDbf(filePath, entries, dbf, properties);

            return true;
        }

        private void AddRecordsToDbf(string filePath, IEnumerable<T> entries, Dbf dbf, IEnumerable<PropertyInfo> properties)
        {
            foreach (var entry in entries)
            {
                DbfRecord record = dbf.CreateRecord();
                var columnCount = 0;
                foreach (PropertyInfo property in properties)
                {
                    record.Data[columnCount] = property.GetValue(entry, null);
                    columnCount++;
                }
            }
            dbf.Write(filePath, DbfVersion.FoxBaseDBase3NoMemo);
        }

        public bool CreateOrUpdateFile(string filePath, IEnumerable<T> entries)
        {
            var properties = typeof(T).GetProperties();
            return !File.Exists(filePath) ? CreateFile(filePath, entries, properties) : UpdateFile(filePath, entries, properties);
        }
    }
}
