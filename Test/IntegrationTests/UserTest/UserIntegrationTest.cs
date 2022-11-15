using Microsoft.AspNetCore.Mvc.Testing;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using Shared.Interfaces.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Test.Fixture;

namespace Test.IntegrationTests.UserTest
{

    [Collection(TestCollection.Name)]
    public class UserIntegrationTest :
    IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        private readonly CustomWebApplicationFactory<Program>
            _factory;

        public UserIntegrationTest(
            CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }

        //public UserIntegrationTest(TestHostFixture testHostFixture)
        //{
        //    httpClient = testHostFixture.httpClient;

        //}

        [Theory]
        [InlineData("/api/user/getCurrent")]
        public async Task Test1(string api)
        {
            var data = JsonConvert.SerializeObject(new { Email = "andjela@gmail.com", Password = "0000" });
            StringContent stringContent = new StringContent(data, Encoding.UTF8,
                                    "application/json");
            var tokenResult = await _httpClient.PostAsync("/api/login/login", stringContent);
            var content = await tokenResult.Content.ReadAsStringAsync();
            Assert.True(tokenResult.StatusCode == HttpStatusCode.OK);

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", content.ToString());

            var userData = await _httpClient.GetAsync(api);
            Assert.NotNull(userData);

            var userList = JsonConvert.DeserializeObject<List<IUser>>(userData.Content.ToString());
            Assert.NotEmpty(userList);
        }
    }

}
