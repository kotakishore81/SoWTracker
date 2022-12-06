using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.DAL
{
    /// <summary>
    /// This structure is used to set the Column Ordinal
    /// </summary>
    [ExcludeFromCodeCoverage]
    struct Prop
    {
        public int ColumnOrdinal { get; set; }
        public Action<object, object> Setter { get; set; }
    }
}
