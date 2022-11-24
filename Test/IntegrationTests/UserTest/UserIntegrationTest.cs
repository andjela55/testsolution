using DTO.Incoming;
using DTO.Outgoing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Test.Fixture;
using Test.Helpers;

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


        [Theory]
        [InlineData("/api/user/getCurrent")]
        public async Task CurrentUserTest(string api)
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
            content = await userData.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDto>(content);
            Assert.NotNull(user);
            Assert.Equal(typeof(UserDto), user.GetType());

        }
        [Theory]
        [ClassData(typeof(UserTheoryData))]
        public async Task InsertUserTest(UserInsertDto userData)
        {
            //logging in
            var data = JsonConvert.SerializeObject(new { Email = "andjela@gmail.com", Password = "0000" });
            StringContent stringContent = new StringContent(data, Encoding.UTF8,
                                    "application/json");
            var tokenResult = await _httpClient.PostAsync("/api/login/login", stringContent);
            var content = await tokenResult.Content.ReadAsStringAsync();
            Assert.True(tokenResult.StatusCode == HttpStatusCode.OK);

            //request headers
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", content.ToString());
            //post new user data
            if (userData != null)
            {
                var result = await _httpClient.PostAsJsonAsync("/api/user", userData);
                Assert.True(result.StatusCode == HttpStatusCode.OK);
                content = await result.Content.ReadAsStringAsync();
                Assert.NotNull(content);
            }

        }

    }

}
