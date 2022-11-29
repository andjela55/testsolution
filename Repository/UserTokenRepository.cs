using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Model.ContextFolder;
using Model.UserTokenClass;
using Shared.Enums;
using Shared.Interfaces.Models;
using SharedRepository;
using System.IdentityModel.Tokens.Jwt;

namespace Repository
{
    public class UserTokenRepository : BaseRepository<UserToken, IUserToken>, IUserTokenRepository
    {
        private Context _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserTokenRepository(Context context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Create(IUserToken userToken)
        {
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
            await UpdateEntity(tokenForUpdate, id);
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
        private long GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.First(i => i.Type == "Id").Value;
            return (long)Convert.ToDouble(userId);
        }
        public async Task<IUserToken> GetRefreshTokenByValue(string refreshToken)
        {
            var userIdString = GetClaimFromToken(refreshToken, "Id");
            var userId = (long)Convert.ToDouble(userIdString);

            var refreshTokenDb = await _context.UserTokens.Where(x => x.TokenType == TokenType.RefreshToken
                                                                   && x.Token == refreshToken
                                                                   && x.IsUsed == false
                                                                   && x.UserId == userId).FirstOrDefaultAsync();
            return refreshTokenDb;
        }
        public async Task SetUsedRefreshToken(IUserToken refreshToken)
        {
            //var refreshTokenDb = await _context.UserTokens.Where(x => x.Id == refreshToken.Id).FirstOrDefaultAsync();
            var refreshTokenDb = _mapper.Map<UserToken>(refreshToken);
            refreshTokenDb.IsUsed = true;
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        private string GetClaimFromToken(string token, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var claimValue = tokenS.Claims.First(claim => claim.Type.Equals(claimType)).Value;
            return claimValue;
        }

    }
}
