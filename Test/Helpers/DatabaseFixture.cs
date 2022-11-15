//using System;
//using System.IO;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Reflection;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc.ApplicationParts;
//using Microsoft.AspNetCore.Mvc.Controllers;
//using Microsoft.AspNetCore.Mvc.ViewComponents;
//using Microsoft.AspNetCore.TestHost;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Services;

//namespace WideWorldImporters.API.IntegrationTests
//{
//    public class TestFixture<TStatup> : IDisposable
//    {
//        /// <summary>
//        /// Get the application project path where the startup assembly lives
//        /// </summary>    
//        string GetProjectPath(string projectRelativePath, Assembly startupAssembly)
//        {
//            var projectName = startupAssembly.GetName().Name;

//            var applicationBaseBath = AppContext.BaseDirectory;

//            var directoryInfo = new DirectoryInfo(applicationBaseBath);

//            do
//            {
//                directoryInfo = directoryInfo.Parent;
//                var projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));
//                if (projectDirectoryInfo.Exists)
//                {
//                    if (new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj")).Exists)
//                        return Path.Combine(projectDirectoryInfo.FullName, projectName);
//                }
//            } while (directoryInfo.Parent != null);

//            throw new Exception($"Project root could not be located using application root {applicationBaseBath}");
//        }

//        /// <summary>
//        /// The temporary test server that will be used to host the controllers
//        /// </summary>
//        private TestServer _server;

//        /// <summary>
//        /// The client used to send information to the service host server
//        /// </summary>
//        public HttpClient HttpClient { get; }

//        public TestFixture() : this(Path.Combine(""))
//        { }

//        protected TestFixture(string relativeTargetProjectParentDirectory)
//        {
//            var startupAssembly = typeof(TStatup).GetTypeInfo().Assembly;
//            var contentRoot = GetProjectPath(relativeTargetProjectParentDirectory, startupAssembly);

//            var configurationBuilder = new ConfigurationBuilder()
//                .SetBasePath(contentRoot)
//                .AddJsonFile("appsettings.json")
//                .AddJsonFile("appsettings.Development.json");


//            var webHostBuilder = new WebHostBuilder()
//                .UseContentRoot(contentRoot)
//                .ConfigureServices(InitializeServices)
//                .UseConfiguration(configurationBuilder.Build())
//                .UseEnvironment("Development")
//                .UseStartup(typeof(TStatup));

//            ////create test instance of the server
//            var startup = new Startup(builder.Configuration);
//            var server = new TestServer(new WebHostBuilder()
//                            .UseEnvironment("Development")
//                            .UseStartup<>());

//            //configure client
//            HttpClient = _server.CreateClient();
//            HttpClient.BaseAddress = new Uri("https://localhost:7158");
//            HttpClient.DefaultRequestHeaders.Accept.Clear();
//            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//        }

//        /// <summary>
//        /// Initialize the services so that it matches the services used in the main API project
//        /// </summary>
//        protected virtual void InitializeServices(IServiceCollection services)
//        {
//            var startupAsembly = typeof(TStatup).GetTypeInfo().Assembly;
//            var manager = new ApplicationPartManager
//            {
//                ApplicationParts = {
//                new AssemblyPart(startupAsembly)
//            },
//                FeatureProviders = {
//                new ControllerFeatureProvider()
//            }
//            };
//            services.AddSingleton(manager);
//        }

//        /// <summary>
//        /// Dispose the Client and the Server
//        /// </summary>
//        public void Dispose()
//        {
//            HttpClient.Dispose();
//            _server.Dispose();
//            _ctx.Dispose();
//        }

//        AppDbContext _ctx = null;
//        public void SeedDataToContext()
//        {
//            if (_ctx == null)
//            {
//                _ctx = _server.Services.GetService<AppDbContext>();
//                if (_ctx != null)
//                    _ctx.SeedAppDbContext();
//            }
//        }
//    }