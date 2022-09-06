using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.ContextFolder;
using Model.UserClass;
using Shared.Interfaces.Models;
using SharedRepository;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private Context _context;
        private readonly IMapper _mapper;
        public UserRepository(Context context, IMapper mapper)
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
        public async Task<List<IUser>> GetAll()
        {
            var users = await _context.Users.ToListAsync<IUser>();
            return users;
        }
        public async Task<bool> Insert(IUser user)
        {
            var userForInsert = _mapper.Map<User>(user);
            await _context.Users.AddAsync(userForInsert);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IUser> GetByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
            return user;
        }
        public async Task<bool> Update(long idUser, IUser userForUpdate)
        {
            var userFromDb = _context.Users.FirstOrDefault(x => x.Id == idUser);
            if (userFromDb == null)
            {
                throw new DbUpdateException("No data to be updated.");
            }
            _context.Entry(userFromDb).CurrentValues.SetValues(userForUpdate);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
