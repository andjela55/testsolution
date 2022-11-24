using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Models.LoginResponseClass;
using Services.Models.UserClass;
using Services.Models.UserTokenClass;
using Shared.Enums;
using Shared.Exceptions;
using Shared.Interfaces.Models;
using SharedRepository;
using SharedServices;
using SharedServices.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Services
{
    public class LoginService : ILoginService
    {

        private IConfiguration _config;
        private IUserRoleRepository _userRoleRepository;
        private IRolePermissionRepository _rolePermissionRepository;
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IHashService _hashService;
        private IUserTokenRepository _userTokenRepository;

        public LoginService(IConfiguration config,
                            IUserRoleRepository userRoleRepository,
                            IRolePermissionRepository rolePermissionRepository,
                            IUserRepository userRepository,
                            IMapper mapper,
                            IHashService hashService,
                            IUserTokenRepository userTokenRepository)
        {
            _config = config;
            _userRoleRepository = userRoleRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _hashService = hashService;
            _userTokenRepository = userTokenRepository;
        }

        public async Task<ILoginResponse> LoginUser(ILogin loginData)
        {
            var user = await AuthenticateUser(loginData.Email, loginData.Password);
            if (user == null)
            {
                throw new Exception("User doesn't exist.");
            }
            if (!user.AccountConfirmed)
            {
                throw new Exception("Your registration is not confirmed.");
            }
            var token = await GenerateJSONWebToken(user.Id);
            if (token == null)
            {
                throw new Exception("Error while generating token");
            }
            var refreshToken = _hashService.GenerateRandomString();
            var newUserTokenItem = new ServicesUserToken()
            {
                TokenType = TokenType.RefreshToken,
                ExpirationDate = DateTime.UtcNow.AddDays(1),
                UserId = user.Id,
                Token = refreshToken
            };
            await _userTokenRepository.Create(newUserTokenItem);
            var response = new ServiceLoginResponse()
            {
                JwtToken = token,
                RefreshToken = refreshToken
            };
            return response;
        }
        private async Task<string> GenerateJSONWebToken(long id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var rolesIds = await _userRoleRepository.GetUserRolesIdsByUserId(id);
            var permissions = await _rolePermissionRepository.GetPermissionIdsByRoleIds(rolesIds);

            var claims = new[] {
            new Claim("Id", id.ToString()),
            new Claim("Permissions",JsonSerializer.Serialize(permissions)),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<IUser> AuthenticateUser(string email, string password)
        {
            var userByEmail = await _userRepository.GetByEmail(email);
            if (userByEmail == null)
            {
                throw new UnauthorizedAccessException("User with forwarded email does not exist");
            }
            if (userByEmail.Password == null)
            {
                return null;
            }
            var salt = userByEmail.Salt;
            password = _hashService.HashPassword(password, salt, 20, 20);
            var user = await _userRepository.AuthenticateUser(email, password);

            if (user == null)
            {
                throw new UnauthorizedAccessException("User with forwarded credentials does not exist");
            }
            return user;
        }
        public async Task<bool> SetInitialPassword(ILogin data)
        {
            try
            {
                var salt = _hashService.GenerateRandomString();
                var userDb = await _userRepository.GetByEmail(data.Email);
                var user = _mapper.Map<ServicesUser>(userDb);
                user.Password = _hashService.HashPassword(data.Password, salt, 20, 20);
                user.Salt = salt;
                await _userRepository.UpdateUser(user);
            }
            catch (Exception)
            {
                throw new BadRequestException("Error while updating data.");
            }
            return true;
        }
        public async Task<ILoginResponse> RefreshTokens(ILoginResponse loginData)
        {

            //check refresh token
            var oldRefreshToken = await _userTokenRepository.GetRefreshTokenByValue(loginData.RefreshToken);
            if (oldRefreshToken == null)
            {
                throw new BadRequestException("Credentials are not correct, please log in again.");
            }
            //set old refresh token to USED
            await _userTokenRepository.SetUsedRefreshToken(oldRefreshToken);

            //create new JWT
            var token = await GenerateJSONWebToken(oldRefreshToken.UserId);
            if (token == null)
            {
                throw new Exception("Error while generating token");
            }
            //create new refresh token
            var newRefreshToken = _hashService.GenerateRandomString();

            var newUserTokenItem = new ServicesUserToken()
            {
                TokenType = TokenType.RefreshToken,
                ExpirationDate = DateTime.UtcNow.AddDays(1),
                UserId = oldRefreshToken.UserId,
                Token = newRefreshToken
            };
            //add new refresh token to db
            await _userTokenRepository.Create(newUserTokenItem);

            //return new tokens
            var response = new ServiceLoginResponse()
            {
                JwtToken = token,
                RefreshToken = newRefreshToken
            };
            return response;
        }

    }
}

