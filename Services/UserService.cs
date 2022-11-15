using AutoMapper;
using Microsoft.AspNetCore.Http;
using Model;
using Model.ContextFolder;
using Model.UserClass;
using Services.Models.UserClass;
using Shared.Constants;
using Shared.Exceptions;
using Shared.Interfaces.Models;
using SharedRepository;
using SharedServices.Interfaces;
using System.Text.Json;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Services
{
    public class UserService : IUserService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IMemoryCacheService _memoryCacheService;
        private IUserRepository _userRepository;
        private IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;
        private Context _context;
        public UserService(
                           IHttpContextAccessor httpContextAccessor,
                           IUserRepository userRepository,
                           IUserRoleRepository userRoleRepository,
                           IRoleRepository roleRepository,
                           IMemoryCacheService memoryCacheService,
                           IMapper mapper,
                           Context context
                         )
        {
            _httpContextAccessor = httpContextAccessor;
            _memoryCacheService = memoryCacheService;
            _userRepository = userRepository;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
            _context = context;
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
            //using TransactionScope transactionScope = new TransactionScope();
            //try
            //{
            //    var userInserted = await _userRepository.Create(userForInsert);
            //    foreach (var roleId in user.Roles)
            //    {
            //        var userRole = new UserRole { RoleId = roleId, UserId = userInserted.Id };
            //        await _userRoleRepository.Create(userRole);
            //    }
            //    _memoryCacheService.RemoveItem(MemoryAttributeConstants.GetAllUsers);
            //    transactionScope.Complete();
            //    transactionScope.Dispose();
            //}
            //catch (TransactionException ex)
            //{
            //    transactionScope.Dispose();
            //}
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var userInserted = await _userRepository.Create(userForInsert);
                    foreach (var roleId in user.Roles)
                    {
                        var userRole = new UserRole { RoleId = roleId, UserId = userInserted.Id };
                        await _userRoleRepository.Create(userRole);
                    }
                    _memoryCacheService.RemoveItem(MemoryAttributeConstants.GetAllUsers);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
                return true;
            }
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
           
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                try
                {
                    var userForUpdate = _mapper.Map<ServicesUser>(user);
                    await _userRepository.UpdateUser(userForUpdate);
                    await _userRoleRepository.AddForUser(userForUpdate.Id, user.Roles);

                    _memoryCacheService.RemoveItem(MemoryAttributeConstants.GetAllUsers);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            
           
            //    using TransactionScope transactionScope = new TransactionScope();
            //try
            //{
            //    var userForUpdate = _mapper.Map<ServicesUser>(user);
            //    await _userRepository.UpdateUser(userForUpdate);
            //    await _userRoleRepository.AddForUser(userForUpdate.Id, user.Roles);

            //    _memoryCacheService.RemoveItem(MemoryAttributeConstants.GetAllUsers);
            //    transactionScope.Complete();
            //    transactionScope.Dispose();
            //}
            //catch (TransactionException ex)
            //{
            //    transactionScope.Dispose();
            }
            return true;
        }
        public async Task<IUser> GetByEmail(string email)
        {
            var result = await _userRepository.GetByEmail(email);
            return result;
        }

    }
}
