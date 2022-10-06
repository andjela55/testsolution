using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.ContextFolder;
using Model.UserClass;
using Shared.Interfaces.Models;
using SharedRepository;

namespace Repository
{
    public class UserRepository : BaseRepository<User, IUser>, IUserRepository
    {
        private Context _context;
        private readonly IMapper _mapper;
        public UserRepository(Context context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IUser> AuthenticateUser(string email, string password)
        {
            var user = await _context.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
            return user;
        }
        public async Task<IUser> GetById(long userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return user;
        }
        public async Task<List<IUser>> GetAllUsers()
        {
            var users = await GetEntities();
            return users.ToList<IUser>();
        }
        public async Task<IUser> Create(IUser user)
        {
            var insertedUser = await InsertEntity(user);
            return insertedUser;
        }
        public async Task<IUser> GetByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
            return user;
        }
        public async Task<bool> UpdateUser(IUser userForUpdate)
        {
            //var userFromDb = _context.Users.FirstOrDefault(x => x.Id == idUser);
            //if (userFromDb == null)
            //{
            //    throw new DbUpdateException("No data to be updated.");
            //}
            //_context.Entry(userFromDb).CurrentValues.SetValues(userForUpdate);
            //await _context.SaveChangesAsync();
            //var updateValues = _mapper.Map<User>(userForUpdate);
            await UpdateEntity(userForUpdate);
            return true;
        }
    }
}
