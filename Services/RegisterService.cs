using AutoMapper;
using Microsoft.Extensions.Options;
using Services.Models.UserClass;
using Services.Models.UserTokenClass;
using Shared.HelperClasses;
using Shared.Interfaces.Models;
using SharedRepository;
using SharedServices;
using SharedServices.Interfaces;
using System.Transactions;

namespace Services
{
    public class RegisterService : IRegisterService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IHashService _hashService;
        private IEmailHelperService _emailService;
        private IUserTokenRepository _userTokenRepository;
        private readonly VariableConfigObject _configObject;
        public RegisterService(
                            IUserRepository userRepository,
                            IMapper mapper,
                            IHashService hashService,
                            IEmailHelperService emailService,
                            IUserTokenRepository userTokenRepository,
            IOptions<VariableConfigObject> configObject)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _hashService = hashService;
            _emailService = emailService;
            _userTokenRepository = userTokenRepository;
            _configObject = configObject.Value;
        }
        public async Task RegisterUser(IUserInsert userData)
        {
            var existingUserWithEmail = await _userRepository.GetByEmail(userData.Email);
            if (existingUserWithEmail != null)
            {
                throw new Exception($"User with email [{userData.Email}] already exists!");
            }
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var salt = _hashService.GenerateSalt();
            var user = _mapper.Map<User>(userData);
            user.Password = _hashService.HashPassword(userData.Password, salt, 20, 20);
            user.Salt = salt;


            var insertedUser = await _userRepository.Insert(user);

            var userToken = new UserToken()
            {
                UserId = insertedUser.Id,
                Token = Guid.NewGuid().ToString(),
                ExpirationDate = DateTime.UtcNow.AddDays(1),
                TokenType = Shared.Enums.TokenType.VerificationToken
            };
            await _userTokenRepository.Insert(userToken);
            var pageLink = _configObject.PageLink + $"?id={insertedUser.Id}&token={userToken.Token}";

            bool emailResponse = _emailService.SendEmail(user.Email, pageLink);
            if (!emailResponse)
            {
                throw new Exception("Greska prilikom slanja mejla.");
            }
            scope.Complete();
        }
        public async Task<bool> ConfirmRegistration(long id, string token)
        {
            var registrationToken = await _userTokenRepository.GetRegistrationTokenForUser(id);

            if (registrationToken == null)
            {
                throw new Exception("User is not registered.");
            }
            if (!registrationToken.Token.Equals(token))
            {
                throw new Exception("User credentials are not correct.");
            }
            if (registrationToken.ExpirationDate < DateTime.UtcNow)
            {
                throw new Exception("Registration token is expired.");
            }
            if (registrationToken.IsUsed)
            {
                throw new Exception("Registration token is already used.");
            }
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var registrationTokenForUpdate = _mapper.Map<UserToken>(registrationToken);
            registrationTokenForUpdate.IsUsed = true;
            await _userTokenRepository.Update(registrationToken.Id, registrationTokenForUpdate);

            var userFromDb = await _userRepository.GetById(id);
            var userForUpdate = _mapper.Map<User>(userFromDb);
            userForUpdate.AccountConfirmed = true;

            await _userRepository.Update(id, userForUpdate);
            scope.Complete();
            return true;
        }
    }
}
