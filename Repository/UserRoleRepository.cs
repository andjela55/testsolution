using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.ContextFolder;
using SharedRepository;

namespace Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private Context _context;
        private readonly IMapper _mapper;
        public UserRoleRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddRolesForUser(long userId, IEnumerable<long> roles)
        {
            foreach (var ur in roles)
            {
                var userRole = new UserRole { UserId = userId, RoleId = ur };
                _context.UserRoles.Add(userRole);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<long>> GetUserRolesIdsByUserId(long userId)
        {
            var rolesId = await _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToListAsync();
            return rolesId;
        }
        public async Task<bool> Insert(long userId, long roleId)
        {
            var userRole = new UserRole { UserId = userId, RoleId = roleId };
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
