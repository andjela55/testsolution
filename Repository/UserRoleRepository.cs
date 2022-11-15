using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.ContextFolder;
using Model.UserClass;
using Shared.Interfaces.Models;
using SharedRepository;

namespace Repository
{
    public class UserRoleRepository : BaseRepository<UserRole, IUserRole>, IUserRoleRepository
    {
        private Context _context;
        private readonly IMapper _mapper;
        public UserRoleRepository(Context context, IMapper mapper):base(context,mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddForUser(long userId, IEnumerable<long> roles)
        {
            var elementsToDelete = await _context.UserRoles.Where(x => x.UserId == userId).ToListAsync();
            if (elementsToDelete.Any())
            {
                _context.RemoveRange(elementsToDelete);
                await _context.SaveChangesAsync();
            }
            _context.UserRoles.AddRange(roles.Select(x => new UserRole { UserId = userId, RoleId = x }));
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<long>> GetUserRolesIdsByUserId(long userId)
        {
            var rolesId = await _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToListAsync();
            return rolesId;
        }

        public async Task<IUserRole> Create(IUserRole userRole)
        {
            var userRoleInsert = await InsertEntity(userRole);
            return userRoleInsert;
        }
     
    }
}
