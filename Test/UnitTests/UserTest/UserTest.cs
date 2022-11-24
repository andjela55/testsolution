using AutoMapper;
using Microsoft.AspNetCore.Http;
using Model.UserClass;
using Moq;
using Services;
using Shared.Interfaces.Models;
using SharedRepository;
using SharedServices;
using SharedServices.Interfaces;

namespace Test.UnitTests.UserTest
{
    public class UserTest
    {
        private Mock<IHttpContextAccessor> httpContextAccessor;
        private Mock<IUserRepository> userRepository;
        private Mock<IUserRoleRepository> userRoleRepository;
        private Mock<IRoleRepository> roleRepository;
        private Mock<IMemoryCacheService> memoryCacheService;
        private Mock<IMapper> mapper;
        private UserService service;
        private Mock<IHashService> hashService;
        public UserTest()
        {
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            userRepository = new Mock<IUserRepository>();
            userRoleRepository = new Mock<IUserRoleRepository>();
            roleRepository = new Mock<IRoleRepository>();
            memoryCacheService = new Mock<IMemoryCacheService>();
            mapper = new Mock<IMapper>();
            hashService = new Mock<IHashService>();
            service = new UserService(httpContextAccessor.Object, userRepository.Object, userRoleRepository.Object, roleRepository.Object, memoryCacheService.Object, mapper.Object, hashService.Object);
        }
        [Fact]
        public async Task GetUserByIdTest()
        {
            userRepository.Setup(x => x.GetById(2)).ReturnsAsync(new User { Id = 2, Name = "Testing user" });

            var firstMockId = 2;
            var result = await service.GetById(firstMockId);
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);

            var secondMockId = -1;
            result = await service.GetById(secondMockId);
            Assert.True(result == null);

            await Task.CompletedTask;
        }


        [Fact]
        public async Task GetAllTest()
        {
            var mockList = new List<IUser>();
            mockList.Add(new User { Id = 1, Name = "All users response" });
            userRepository.Setup(x => x.GetAllUsers()).ReturnsAsync(mockList);

            var result = await service.GetAll();

            Assert.IsType<List<IUser>>(result);
            Assert.NotNull(result);
            await Task.CompletedTask;
        }
    }

}
