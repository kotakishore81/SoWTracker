using Microsoft.EntityFrameworkCore;
using SoW.Tracker.WebAPI.DAL;
using SoW.Tracker.WebAPI.DBInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.DBContext
{
    [ExcludeFromCodeCoverage]
    public static class DbContextExtension
    {
        /// <summary>
        /// Load a stored procedure
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="name">Procedure's name</param>
        /// <returns></returns>
        public static IStoredProcBuilder LoadStoredProc(this DbContext ctx, string name)
        {
            return new StoredProcBuilder(ctx, name);
        }
    }
}
