using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.DAL
{
    [ExcludeFromCodeCoverage]
    public static class DbDataReaderExtension
    {
        private const string NoElementError = "Sequence contains no element";
        private const string MoreThanOneElementError = "Sequence contains more than one element";

        /// <summary>
        /// Map data source to a list
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DbDataReader reader) where T : class, new()
        {
            var res = new List<T>();
            try
            {
                var mapper = new Mapper<T>(reader);
                mapper.Map(row => res.Add(row));
            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Map data source to a list
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static async Task<List<T>> ToListAsync<T>(this DbDataReader reader) where T : class, new()
        {
            var res = new List<T>();
            try
            {
                var mapper = new Mapper<T>(reader);
                await mapper.MapAsync(row => res.Add(row));
            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Map the first column to a list
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<T> Column<T>(this DbDataReader reader) where T : IComparable
        {
            return Column<T>(reader, 0);
        }

        /// <summary>
        /// Map the specified column to a list
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="reader"></param>
        /// <param name="columnName">Name of the column to read. Use first column if null</param>
        /// <returns></returns>
        public static List<T> Column<T>(this DbDataReader reader, string columnName) where T : IComparable
        {
            int ordinal = columnName is null ? 0 : reader.GetOrdinal(columnName);
            return Column<T>(reader, ordinal);
        }

        /// <summary>
        /// Map the specified column to a list
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="reader"></param>
        /// <param name="ordinal">Zero-based column ordinal</param>
        /// <returns></returns>
        public static List<T> Column<T>(this DbDataReader reader, int ordinal) where T : IComparable
        {
            var res = new List<T>();
            try
            {
                while (reader.Read())
                {
                    T value = reader.IsDBNull(ordinal) ? default(T) : (T)reader.GetValue(ordinal);
                    res.Add(value);
                }
            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Map the first column to a list
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Task<List<T>> ColumnAsync<T>(this DbDataReader reader) where T : IComparable
        {
            return ColumnAsync<T>(reader, 0);
        }

        /// <summary>
        /// Map the specified column to a list
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="reader"></param>
        /// <param name="columnName">Name of the column to read. Use first column if null</param>
        /// <returns></returns>
        public static Task<List<T>> ColumnAsync<T>(this DbDataReader reader, string columnName) where T : IComparable
        {
            int ordinal = columnName is null ? 0 : reader.GetOrdinal(columnName);
            return ColumnAsync<T>(reader, ordinal);
        }

        /// <summary>
        /// Map the specified column to a list
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="reader"></param>
        /// <param name="ordinal">Zero-based column ordinal</param>
        /// <returns></returns>
        public static async Task<List<T>> ColumnAsync<T>(this DbDataReader reader, int ordinal) where T : IComparable
        {
            var res = new List<T>();
            try
            {
                while (await reader.ReadAsync())
                {
                    T value = await reader.IsDBNullAsync(ordinal) ? default(T) : (T)reader.GetValue(ordinal);
                    res.Add(value);
                }
            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Create a dictionary. Keys must be unique
        /// </summary>
        /// <typeparam name="TKey">Type of the keys</typeparam>
        /// <typeparam name="TValue">Type of the values</typeparam>
        /// <param name="reader"></param>
        /// <param name="keyProjection">Projection to get the key</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this DbDataReader reader, Func<TValue, TKey> keyProjection) where TKey : IComparable where TValue : class, new()
        {
            var res = new Dictionary<TKey, TValue>();
            try
            {
                var mapper = new Mapper<TValue>(reader);
                mapper.Map(val =>
                {
                    TKey key = keyProjection(val);
                    res[key] = val;
                });
            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Create a dictionary. Keys must be unique
        /// </summary>
        /// <typeparam name="TKey">Type of the keys</typeparam>
        /// <typeparam name="TValue">Type of the values</typeparam>
        /// <param name="keyProjection">Projection to get the key</param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(this DbDataReader reader, Func<TValue, TKey> keyProjection) where TKey : IComparable where TValue : class, new()
        {
            var res = new Dictionary<TKey, TValue>();
            try
            {
                var mapper = new Mapper<TValue>(reader);
                await mapper.MapAsync(val =>
                {
                    TKey key = keyProjection(val);
                    res[key] = val;
                });
            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Create a dictionary
        /// </summary>
        /// <typeparam name="TKey">Type of the keys</typeparam>
        /// <typeparam name="TValue">Type of the values</typeparam>
        /// <param name="reader"></param>
        /// <param name="keyProjection">Projection to get the key</param>
        /// <returns></returns>
        public static Dictionary<TKey, List<TValue>> ToLookup<TKey, TValue>(this DbDataReader reader, Func<TValue, TKey> keyProjection) where TKey : IComparable where TValue : class, new()
        {
            var res = new Dictionary<TKey, List<TValue>>();
            try
            {
                var mapper = new Mapper<TValue>(reader);
                mapper.Map(val =>
                {
                    TKey key = keyProjection(val);

                    if (res.ContainsKey(key))
                    {
                        res[key].Add(val);
                    }
                    else
                    {
                        res[key] = new List<TValue>() { val };
                    }
                });
            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Create a dictionary
        /// </summary>
        /// <typeparam name="TKey">Type of the keys</typeparam>
        /// <typeparam name="TValue">Type of the values</typeparam>
        /// <param name="reader"></param>
        /// <param name="keyProjection">Projection to get the key</param>
        /// <returns></returns>
        public static async Task<Dictionary<TKey, List<TValue>>> ToLookupAsync<TKey, TValue>(this DbDataReader reader, Func<TValue, TKey> keyProjection) where TKey : IComparable where TValue : class, new()
        {
            var res = new Dictionary<TKey, List<TValue>>();
            try
            {
                var mapper = new Mapper<TValue>(reader);
                await mapper.MapAsync(val =>
                {
                    TKey key = keyProjection(val);

                    if (res.ContainsKey(key))
                    {
                        res[key].Add(val);
                    }
                    else
                    {
                        res[key] = new List<TValue>() { val };
                    }
                });
            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Create a set with the first column
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static HashSet<T> ToSet<T>(this DbDataReader reader) where T : IComparable
        {
            var res = new HashSet<T>();
            try
            {
                while (reader.Read())
                {
                    T val = (T)reader.GetValue(0);
                    res.Add(val);
                }
            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Create a set with the first column
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static async Task<HashSet<T>> ToSetAsync<T>(this DbDataReader reader) where T : IComparable
        {
            var res = new HashSet<T>();
            try
            {
                while (await reader.ReadAsync())
                {
                    T val = (T)reader.GetValue(0);
                    res.Add(val);
                }
            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Read first row
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T First<T>(this DbDataReader reader) where T : class, new()
        {
            return First<T>(reader, false, false);
        }

        /// <summary>
        /// Read first row or return default value if the data source is empty
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T FirstOrDefault<T>(this DbDataReader reader) where T : class, new()
        {
            return First<T>(reader, true, false);
        }

        /// <summary>
        /// Read first row or throw an exception if data source contains more than one row
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T Single<T>(this DbDataReader reader) where T : class, new()
        {
            return First<T>(reader, false, true);
        }

        /// <summary>
        /// Read first row or return default value if the data source is empty
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T SingleOrDefault<T>(this DbDataReader reader) where T : class, new()
        {
            return First<T>(reader, true, true);
        }

        /// <summary>
        /// Read first row
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Task<T> FirstAsync<T>(this DbDataReader reader) where T : class, new()
        {
            return FirstAsync<T>(reader, false, false);
        }

        /// <summary>
        /// Read first row or return default value if the data source is empty
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Task<T> FirstOrDefaultAsync<T>(this DbDataReader reader) where T : class, new()
        {
            return FirstAsync<T>(reader, true, false);
        }

        /// <summary>
        /// Read first row or throw an exception if data source contains more than one row
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Task<T> SingleAsync<T>(this DbDataReader reader) where T : class, new()
        {
            return FirstAsync<T>(reader, false, true);
        }

        /// <summary>
        /// Read first row or return default value if the data source is empty
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Task<T> SingleOrDefaultAsync<T>(this DbDataReader reader) where T : class, new()
        {
            return FirstAsync<T>(reader, true, true);
        }

        private static T First<T>(DbDataReader reader, bool orDefault, bool throwIfNotSingle) where T : class, new()
        {
            if (reader.Read())
            {
                var mapper = new Mapper<T>(reader);
                T row = mapper.MapNextRow();

                if (throwIfNotSingle && reader.Read())
                {
                    throw new InvalidOperationException(MoreThanOneElementError);
                }

                return row;
            }

            if (orDefault)
            {
                return default(T);
            }

            throw new InvalidOperationException(NoElementError);
        }

        private static async Task<T> FirstAsync<T>(DbDataReader reader, bool orDefault, bool throwIfNotSingle) where T : class, new()
        {
            if (await reader.ReadAsync())
            {
                var mapper = new Mapper<T>(reader);
                T row = await mapper.MapNextRowAsync();

                if (throwIfNotSingle && await reader.ReadAsync())
                {
                    throw new InvalidOperationException(MoreThanOneElementError);
                }

                return row;
            }

            if (orDefault)
            {
                return default(T);
            }

            throw new InvalidOperationException(NoElementError);
        }
    }
}
