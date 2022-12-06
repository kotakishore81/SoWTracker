using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.DBUtilities
{
    /// <summary>
    /// This Class is used to set the Extra / Optional parameters to call a store proc.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ParamExtra
    {
        public byte Precision { get; set; }
        public byte Scale { get; set; }
        public int Size { get; set; }
    }
}
