using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Models
{
    [ExcludeFromCodeCoverage]
    public class ConnectionStrings
    {
        public string SoWConnectionString { get; set; }
        public bool isDeccrypted { get; set; }
    }
}
