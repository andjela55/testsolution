using Microsoft.EntityFrameworkCore;
using Model;
using Model.ContextFolder;
using SharedRepository;

namespace Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private Context _context;

        public UserRoleRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<long>> GetUserRolesIdsByUserId(long userId)
        {
            var rolesId = await _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToListAsync();
            return rolesId;
        }
        public async Task<bool> Insert(long userId, long roleId)
        {
            var userRole = new UserRole();
            userRole.UserId = userId;
            userRole.RoleId = roleId;
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
