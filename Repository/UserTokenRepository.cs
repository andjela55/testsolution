using AutoMapper;
using Model.ContextFolder;
using Model.UserTokenClass;
using Shared.Interfaces.Models;
using SharedRepository;

namespace Repository
{
    public class UserTokenRepository : IUserTokenRepository
    {
        private Context _context;
        private readonly IMapper _mapper;
        public UserTokenRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Insert(IUserToken userToken)
        {
            var userTokenForInsert = _mapper.Map<UserToken>(userToken);
            await _context.UserTokens.AddAsync(userTokenForInsert);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
