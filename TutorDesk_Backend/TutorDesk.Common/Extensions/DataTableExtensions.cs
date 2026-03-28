using System.Data;
using System.Reflection;

namespace TutorDesk.Common.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Converts a DataTable into a list of strongly typed objects.
        /// Column names must match property names.
        /// </summary>
        public static List<T> Convert<T>(this DataTable table) where T : new()
        {
            var list = new List<T>();


        if (table == null || table.Rows.Count == 0)
                return list;

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (DataRow row in table.Rows)
            {
                var obj = new T();

                foreach (var prop in properties)
                {
                    if (!table.Columns.Contains(prop.Name) || row[prop.Name] == DBNull.Value)
                        continue;

                    prop.SetValue(obj, row[prop.Name]);
                }

                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// Converts first row of DataTable to a single object.
        /// </summary>
        public static T? ConvertSingle<T>(this DataTable table) where T : new()
        {
            if (table == null || table.Rows.Count == 0)
                return default;

            return table.Convert<T>().FirstOrDefault();
        }
    }
}
