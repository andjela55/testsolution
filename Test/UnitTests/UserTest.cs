
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Model.ContextFolder;
using Model.UserClass;
using Moq;
using Services;
using Shared.Interfaces.Models;
using SharedRepository;
using SharedServices;
using SharedServices.Interfaces;

namespace Test.UserTest
{
    public class UserTest
    {
        [Fact]
        public async Task GetUserByIdTest()
        {
            Mock<IHttpContextAccessor> httpContextAccessor = new Mock<IHttpContextAccessor>();
            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
            Mock<IUserRoleRepository> userRoleRepository = new Mock<IUserRoleRepository>();
            Mock<IRoleRepository> roleRepository = new Mock<IRoleRepository>();
            Mock<IMemoryCacheService> memoryCacheService = new Mock<IMemoryCacheService>();
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<Context> context = new Mock<Context>();
            Mock<IHashService> hashService = new Mock<IHashService>();
            hashService = new Mock<IHashService>();
            UserService service = new UserService(httpContextAccessor.Object, userRepository.Object, userRoleRepository.Object, roleRepository.Object, memoryCacheService.Object, mapper.Object, hashService.Object);
            userRepository.Setup(x => x.GetById(2))
                          .ReturnsAsync(new User { Id = 2, Name = "Testing user" });


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
            Mock<IHttpContextAccessor> httpContextAccessor = new Mock<IHttpContextAccessor>();
            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
            Mock<IUserRoleRepository> userRoleRepository = new Mock<IUserRoleRepository>();
            Mock<IRoleRepository> roleRepository = new Mock<IRoleRepository>();
            Mock<IMemoryCacheService> memoryCacheService = new Mock<IMemoryCacheService>();
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<IHashService> hashService = new Mock<IHashService>();
            UserService service = new UserService(httpContextAccessor.Object, userRepository.Object, userRoleRepository.Object, roleRepository.Object, memoryCacheService.Object, mapper.Object, hashService.Object);

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
