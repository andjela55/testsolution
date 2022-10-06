using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.ContextFolder;
using Model.UserTokenClass;
using Shared.Interfaces.Models;
using SharedRepository;


namespace Repository
{
    public class UserTokenRepository : BaseRepository<UserToken, IUserToken>, IUserTokenRepository
    {
        private Context _context;
        private readonly IMapper _mapper;
        public UserTokenRepository(Context context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Create(IUserToken userToken)
        {
            //var userTokenForInsert = _mapper.Map<UserToken>(userToken);
            //await _context.UserTokens.AddAsync(userTokenForInsert);
            //await _context.SaveChangesAsync();
            await InsertEntity(userToken);
            return true;
        }
        public async Task<IUserToken> GetRegistrationTokenForUser(long id)
        {
            var userToken = await _context.UserTokens.Where(x => x.UserId == id && x.TokenType == Shared.Enums.TokenType.VerificationToken).OrderByDescending(x => x.ExpirationDate).FirstOrDefaultAsync();
            return userToken;
        }
        public async Task<bool> Update(long id, IUserToken tokenForUpdate)
        {
            var tokenFromDb = _context.UserTokens.FirstOrDefault(x => x.Id == id);
            if (tokenFromDb == null)
            {
                throw new DbUpdateException("No data to be updated.");
            }
            _context.Entry(tokenFromDb).CurrentValues.SetValues(tokenForUpdate);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DeleteExpiredTokens()
        {
            var tokensForRemoving = _context.UserTokens.Where(x => x.ExpirationDate.AddDays(3) < DateTime.UtcNow);
            if (!tokensForRemoving.Any())
            {
                return true;
            }
            _context.UserTokens.RemoveRange(tokensForRemoving);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
