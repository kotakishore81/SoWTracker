using SoW.Tracker.WebAPI.DBInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.DAL
{
    [ExcludeFromCodeCoverage]
    internal class OutputParam<T> : IOutParam<T>
    {
        public OutputParam(DbParameter param)
        {
            _dbParam = param;
        }

        public T Value => (T)Convert.ChangeType(_dbParam.Value, typeof(T));

        public override string ToString() => _dbParam.Value.ToString();

        private readonly DbParameter _dbParam;
    }
}
