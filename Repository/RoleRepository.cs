using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.ContextFolder;
using Model.RoleClass;
using Shared.Interfaces.Models;
using SharedRepository;

namespace Repository
{
    public class RoleRepository : IRoleRepository
    {

        private Context _context;
        private readonly IMapper _mapper;

        public RoleRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<IRole>> GetAll()
        {

            var roles = await _context.Roles.ToListAsync<IRole>();
            return roles;

        }

        public async Task<bool> Insert(IRole role)
        {
            var roleForInsert = _mapper.Map<Role>(role);
            await _context.Roles.AddAsync(roleForInsert);
            return true;
        }
    }
}
