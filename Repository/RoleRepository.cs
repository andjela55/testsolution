using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.ContextFolder;
using Model.RoleClass;
using Shared.Interfaces.Models;
using SharedRepository;

namespace Repository
{
    public class RoleRepository :BaseRepository<Role,IRole>, IRoleRepository
    {

        private Context _context;
        private readonly IMapper _mapper;

        public RoleRepository(Context context, IMapper mapper):base(context,mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<IRole>> GetAll()
        {

            //var roles = await _context.Roles.ToListAsync<IRole>();
            var roles = await GetEntities();
            return roles.ToList<IRole>();

        }

        public async Task<bool> Insert(IRole role)
        {
            //var roleForInsert = _mapper.Map<Role>(role);
            //await _context.Roles.AddAsync(roleForInsert);
            await InsertEntity(role);
            return true;
        }
    }
}
