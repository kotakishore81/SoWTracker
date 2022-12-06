using SoW.Tracker.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.ServiceInterface
{
   public interface IManageUsers
    {
        [ExcludeFromCodeCoverage]
      public Task<IList<Users>> GetAllUsers();
    }
}
