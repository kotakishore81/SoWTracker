using SoW.Tracker.WebAPI.DAL;
using SoW.Tracker.WebAPI.DBContext;
using SoW.Tracker.WebAPI.Models;
using SoW.Tracker.WebAPI.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.ServiceImplementation
{
    public class ManageUsers : IManageUsers
    {
        private readonly SoWDbContext _context;
        //private readonly ICacheProvider _cache;
        //private readonly IAuthUtility _authUtility;
        //private readonly User _authUser;
        public ManageUsers(SoWDbContext context)
        {
            _context = context;
            //_cache = cache;
            //_authUtility = authUtility;
            //_authUser = _authUtility.GetAuthUserFromRequest();
        }
        public async Task<IList<Users>> GetAllUsers()
        {
            List<Users> lstUsers = null;
            try
            {
                await _context.LoadStoredProc(SP_ManageUser.SP_GETALLUSERS)
                .ExecAsync(async r => lstUsers = await r.ToListAsync<Users>());
            }
            catch
            {
                throw;
            }
            return lstUsers;
        }
    }
}
