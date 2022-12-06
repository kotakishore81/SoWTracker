using Microsoft.EntityFrameworkCore;
using SoW.Tracker.WebAPI.DBInterfaces;
using SoW.Tracker.WebAPI.DBUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.DAL
{
    [ExcludeFromCodeCoverage]
    internal class StoredProcBuilder : IStoredProcBuilder
    {
        private const string _retParamName = "_retParam";
        private readonly DbCommand _cmd;
        private DbTransaction _tran;

        public StoredProcBuilder(DbContext ctx, string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            DbCommand cmd = ctx.Database.GetDbConnection().CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = name;

            int? cmdTimeout = ctx.Database.GetCommandTimeout();
            if (cmdTimeout.HasValue)
            {
                cmd.CommandTimeout = cmdTimeout.Value;
            }

            _cmd = cmd;
        }

        public void ChangeStoredProc(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (_cmd is null)
            {
                throw new ArgumentNullException(nameof(_cmd));
            }
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.CommandText = name;
        }

        public IStoredProcBuilder AddParam<T>(string name, T val)
        {
            AddParamInner(name, val, ParameterDirection.Input, null);
            return this;
        }

        public IStoredProcBuilder AddParam<T>(string name, out IOutParam<T> outParam)
        {
            outParam = AddOutputParamInner(name, default(T), ParameterDirection.Output, null);
            return this;
        }

        public IStoredProcBuilder AddParam<T>(string name, out IOutParam<T> outParam, ParamExtra extra)
        {
            outParam = AddOutputParamInner(name, default(T), ParameterDirection.Output, extra);
            return this;
        }

        public IStoredProcBuilder AddParam<T>(string name, T val, out IOutParam<T> outParam)
        {
            outParam = AddOutputParamInner(name, val, ParameterDirection.InputOutput, null);
            return this;
        }

        public IStoredProcBuilder AddParam<T>(string name, T val, out IOutParam<T> outParam, ParamExtra extra)
        {
            outParam = AddOutputParamInner(name, val, ParameterDirection.InputOutput, extra);
            return this;
        }

        public IStoredProcBuilder ReturnValue<T>(out IOutParam<T> retParam)
        {
            retParam = AddOutputParamInner(_retParamName, default(T), ParameterDirection.ReturnValue, null);
            return this;
        }

        public IStoredProcBuilder ReturnValue<T>(out IOutParam<T> retParam, ParamExtra extra)
        {
            retParam = AddOutputParamInner(_retParamName, default(T), ParameterDirection.ReturnValue, extra);
            return this;
        }

        public IStoredProcBuilder SetTimeout(int timeout)
        {
            _cmd.CommandTimeout = timeout;
            return this;
        }

        public void Exec(Action<DbDataReader> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            try
            {
                OpenConnection();
                using (DbDataReader r = _cmd.ExecuteReader())
                {
                    action(r);
                }
            }
            finally
            {
                Dispose();
            }
        }

        public async Task ExecAsync(Func<DbDataReader, Task> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            try
            {
                try
                {
                    await OpenConnectionAsync();

                    using (DbDataReader r = await _cmd.ExecuteReaderAsync())
                    {
                        await action(r);
                    }
                }
                catch
                {
                    throw;
                }
            }
            finally
            {
                Dispose();
            }
        }

        public void ExecNonQuery()
        {
            try
            {
                OpenConnection();
                _cmd.ExecuteNonQuery();
            }
            finally
            {
                Dispose();
            }
        }

        public async Task ExecNonQueryAsync()
        {
            try
            {
                await OpenConnectionAsync();
                if (_tran != null)
                {
                    _cmd.Transaction = _tran;
                }

                await _cmd.ExecuteNonQueryAsync();
            }
            catch
            {
                throw;
            }

        }

        public void ExecScalar<T>(out T val)
        {
            try
            {
                OpenConnection();
                object scalar = _cmd.ExecuteScalar();
                val = DefaultIfDBNull<T>(scalar);
            }
            finally
            {
                Dispose();
            }
        }

        public async Task ExecScalarAsync<T>(Action<T> action)
        {
            try
            {
                await OpenConnectionAsync();
                object scalar = await _cmd.ExecuteScalarAsync();
                T val = DefaultIfDBNull<T>(scalar);
                action(val);
            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            _cmd.Connection.Close();
            _cmd.Dispose();
        }

        private OutputParam<T> AddOutputParamInner<T>(string name, T val, ParameterDirection direction, ParamExtra extra)
        {
            DbParameter param = AddParamInner(name, val, direction, extra);
            return new OutputParam<T>(param);
        }

        private DbParameter AddParamInner<T>(string name, T val, ParameterDirection direction, ParamExtra extra)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            DbParameter param = _cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = (object)val ?? DBNull.Value;
            param.Direction = direction;
            param.DbType = DbTypeConverter.ConvertToDbType<T>();
            if (extra != null)
            {
                param.Precision = extra.Precision;
                param.Scale = extra.Scale;
                param.Size = extra.Size;
            }

            _cmd.Parameters.Add(param);
            return param;
        }

        private void OpenConnection()
        {
            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                _cmd.Connection.Open();
            }
        }

        private Task OpenConnectionAsync()
        {
            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                return _cmd.Connection.OpenAsync();
            }
            return Task.CompletedTask;
        }
        private T DefaultIfDBNull<T>(object o)
        {
            return o == DBNull.Value ? default(T) : (T)o;
        }
        public void BeginTransaction()
        {
            OpenConnectionAsync();
            if (_tran == null)
            {
                _tran = _cmd.Connection.BeginTransaction();
            }
        }
        public void CommitTransaction()
        {
            if (_tran != null)
            {
                _tran.Commit();
            }
        }
        public void RollBackTransaction()
        {
            if (_tran != null)
            {
                _tran.Rollback();
            }
        }
        public void ClearAddedParameters()
        {
            for (int i = _cmd.Parameters.Count - 1; i >= 0; i--)
            {
                _cmd.Parameters.RemoveAt(i);
            }
        }
    }
}
