using AutoMapper;
using Microsoft.Extensions.Configuration;
using Services.Models;
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
        private IConfiguration _configuration;

        public RegisterService(
                            IUserRepository userRepository,
                            IMapper mapper,
                            IHashService hashService,
                            IEmailHelperService emailService,
                            IUserTokenRepository userTokenRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _hashService = hashService;
            _emailService = emailService;
            _userTokenRepository = userTokenRepository;
            _configuration = configuration;
        }
        public async Task RegisterUser(IUserInsert userData)
        {
            var existingUserWithEmail = _userRepository.GetByEmail(userData.Email);
            if (existingUserWithEmail != null)
            {
                throw new Exception($"User with email [{userData.Email}] already exists!");
            }
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);


            var salt = _hashService.GenerateSalt();
            var user = _mapper.Map<User>(userData);
            user.Password = _hashService.HashPassword(userData.Password, salt, 20, 20);
            user.Salt = salt;
            var userToken = new UserToken()
            {
                Token = Guid.NewGuid().ToString(),
                ExpirationDate = DateTime.UtcNow.AddDays(1),
                TokenType = Shared.Enums.TokenType.VerificationToken
            };
            var variables = _configuration.GetSection("Variables");
            var pageLink = variables["RegistrationPageLink"];

            bool emailResponse = _emailService.SendEmail(user.Email, pageLink);
            if (!emailResponse)
            {
                throw new Exception("Greska prilikom slanja mejla.");
            }

            await _userRepository.Insert(user);
            await _userTokenRepository.Insert(userToken);

            scope.Complete();
        }
    }
}
