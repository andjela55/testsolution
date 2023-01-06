using AutoMapper;
using Microsoft.AspNetCore.Http;
using Model;
using Services.Models.UserClass;
using Shared.Constants;
using Shared.Exceptions;
using Shared.Interfaces.Models;
using SharedRepository;
using SharedServices;
using SharedServices.Interfaces;
using System.Text.Json;
using System.Transactions;

namespace Services
{
    public class UserService : IUserService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IMemoryCacheService _memoryCacheService;
        private IUserRepository _userRepository;
        private IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;
        private IHashService _hashService;
        public UserService(
                           IHttpContextAccessor httpContextAccessor,
                           IUserRepository userRepository,
                           IUserRoleRepository userRoleRepository,
                           IRoleRepository roleRepository,
                           IMemoryCacheService memoryCacheService,
                           IMapper mapper,
                           IHashService hashService
                         )
        {
            _httpContextAccessor = httpContextAccessor;
            _memoryCacheService = memoryCacheService;
            _userRepository = userRepository;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
            _hashService = hashService;
        }
        public async Task<IUser> GetCurrentUser()
        {

            var userId = JsonSerializer.Deserialize<long>(_httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == "Id").Value);
            var user = await _userRepository.GetById(userId);

            if (user == null)
            {
                throw new BadRequestException("Nije pronadjen trenutno ulogovani korisnik");

            }
            return user;
        }
        public async Task<List<IUser>> GetAll()
        {
            return await _userRepository.GetAllUsers();
        }
        public async Task<IUser> GetById(long id)
        {
            return await _userRepository.GetById(id);
        }
        public async Task<bool> Insert(IUserInsert user)
        {
            List<string> errors = new List<string>();

            var userForInsert = _mapper.Map<ServicesUser>(user);

            PrepareForInsert(userForInsert);

            await ValidateInsert(userForInsert, errors);

            if (errors.Count > 0)
            {
                throw new BadRequestException("Greska pri kreiranju korisnika");
            }
            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var userInserted = await _userRepository.Create(userForInsert);
                foreach (var roleId in user.Roles)
                {
                    var userRole = new UserRole { RoleId = roleId, UserId = userInserted.Id };
                    await _userRoleRepository.Create(userRole);
                }
                _memoryCacheService.RemoveItem(MemoryAttributeConstants.GetAllUsers);
                scope.Complete();
                scope.Dispose();
            }
            catch (Exception)
            {
                scope.Dispose();
            }
            return true;
        }
        private async Task ValidateInsert(IUser user, List<string> errors)
        {
            var users = await _userRepository.GetAllUsers();
            if (users.Any(x => x.Email == user.Email))
            {
                errors.Add("Greska, email vec postoji");
            }
        }

        public async Task<bool> Update(IUserInsert user)
        {
            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var userForUpdate = _mapper.Map<ServicesUser>(user);
                await _userRepository.UpdateUser(userForUpdate);
                await _userRoleRepository.AddForUser(userForUpdate.Id, user.Roles);

                _memoryCacheService.RemoveItem(MemoryAttributeConstants.GetAllUsers);
                scope.Complete();
                scope.Dispose();
            }
            catch (TransactionAbortedException ex)
            {
                scope.Dispose();
            }

            return true;
        }
        public async Task<IUser> GetByEmail(string email)
        {
            var result = await _userRepository.GetByEmail(email);
            return result;
        }
        private void PrepareForInsert(ServicesUser user)
        {
            user.Salt = _hashService.GenerateRandomString();
            user.Password = _hashService.HashPassword(user.Password, user.Salt, 20, 20);
        }

        public async Task<List<IUser>> GetByIds(IEnumerable<long> ids)
        {
            var users = await _userRepository.GetUsersByIds(ids);
            return users;
        }
    }
}
