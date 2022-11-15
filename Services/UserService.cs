using AutoMapper;
using Microsoft.AspNetCore.Http;
using Model.UserClass;
using Services.Models.UserClass;
using Shared.Constants;
using Shared.Exceptions;
using Shared.Interfaces.Models;
using SharedRepository;
using SharedServices.Interfaces;
using System.Text.Json;

namespace Services
{
    public class UserService : IUserService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IMemoryCacheService _memoryCacheService;
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(
                           IHttpContextAccessor httpContextAccessor,
                           IUserRepository userRepository,
                           IUserRoleRepository userRoleRepository,
                           IRoleRepository roleRepository,
                           IMemoryCacheService memoryCacheService,
                           IMapper mapper
                         )
        {
            _httpContextAccessor = httpContextAccessor;
            _memoryCacheService = memoryCacheService;
            _userRepository = userRepository;
            _mapper = mapper;

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
            await ValidateInsert(userForInsert, errors);

            if (errors.Count > 0)
            {
                throw new BadRequestException("Greska pri kreiranju korisnika");
            }
            else
            {
                await _userRepository.Create(userForInsert);
                _memoryCacheService.RemoveItem(MemoryAttributeConstants.GetAllUsers);
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

        public async Task<bool> Update(IUser user)
        {
            try
            {
                await _userRepository.UpdateUser(user);
            }
            catch (Exception)
            {
                throw new BadRequestException("Greska pri izmeni korisnika");
            }
            return true;
        }
        public async Task<IUser>GetByEmail(string email)
        {
            var result=await _userRepository.GetByEmail(email);
            return result;
        }
    }
}
