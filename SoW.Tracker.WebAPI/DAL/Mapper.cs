﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.DAL
{
    /// <summary>
    /// Mapper <see cref="DbDataReader"/> to model of type <see cref="T"/>
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    [ExcludeFromCodeCoverage]
    internal class Mapper<T> where T : class, new()
    {
        private static ConcurrentDictionary<CacheKey, Prop[]> _propertiesCache = new ConcurrentDictionary<CacheKey, Prop[]>();

        private readonly DbDataReader _reader;
        private readonly Prop[] _properties;

        public Mapper(DbDataReader reader)
        {
            _reader = reader;
            _properties = MapColumnsToProperties();
        }

        /// <summary>
        /// Map <see cref="DbDataReader"/> to a T and apply an action on it for each row
        /// </summary>
        /// <param name="action">Action to apply to each row</param>
        public void Map(Action<T> action)
        {
            while (_reader.Read())
            {
                T row = MapNextRow();
                action(row);
            }
        }

        /// <summary>
        /// Map <see cref="DbDataReader"/> to a T and apply an action on it for each row
        /// </summary>
        /// <param name="action">Action to apply to each row</param>
        public async Task MapAsync(Action<T> action)
        {
            while (await _reader.ReadAsync())
            {
                T row = await MapNextRowAsync();
                action(row);
            }
        }

        public T MapNextRow()
        {
            T row = new T();
            for (int i = 0; i < _properties.Length; ++i)
            {
                object value = _reader.IsDBNull(_properties[i].ColumnOrdinal) ? null : _reader.GetValue(_properties[i].ColumnOrdinal);
                _properties[i].Setter(row, value);
            }
            return row;
        }

        public async Task<T> MapNextRowAsync()
        {
            T row = new T();
            for (int i = 0; i < _properties.Length; ++i)
            {
                object value = await _reader.IsDBNullAsync(_properties[i].ColumnOrdinal) ? null : _reader.GetValue(_properties[i].ColumnOrdinal);
                _properties[i].Setter(row, value);
            }
            return row;
        }

        private Prop[] MapColumnsToProperties()
        {
            Type modelType = typeof(T);

            string[] columns = new string[_reader.FieldCount];
            for (int i = 0; i < _reader.FieldCount; ++i)
            {
                columns[i] = _reader.GetName(i);
            }

            var key = new CacheKey(columns, modelType);
            if (_propertiesCache.TryGetValue(key, out Prop[] s))
            {
                return s;
            }

            var properties = new List<Prop>(columns.Length);
            for (int i = 0; i < columns.Length; i++)
            {
                string name = columns[i].Replace("_", "");
                PropertyInfo prop = modelType.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop == null)
                {
                    continue;
                }

                ParameterExpression instance = Expression.Parameter(typeof(object), "instance");
                ParameterExpression argument = Expression.Parameter(typeof(object), "value");
                MethodCallExpression setterCall = Expression.Call(Expression.Convert(instance, prop.DeclaringType), prop.GetSetMethod(), Expression.Convert(argument, prop.PropertyType));
                var setter = (Action<object, object>)Expression.Lambda(setterCall, instance, argument).Compile();

                properties.Add(new Prop
                {
                    ColumnOrdinal = i,
                    Setter = setter,
                });
            }
            Prop[] propertiesArray = properties.ToArray();
            _propertiesCache[key] = propertiesArray;
            return propertiesArray;
        }
    }
}
