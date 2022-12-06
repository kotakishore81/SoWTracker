using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.DBInterfaces
{
    /// <summary>
    /// This interface is used to set the Out parameters in store procedures.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOutParam<T>
    {
        [ExcludeFromCodeCoverage]
        T Value { get; }
    }
}
