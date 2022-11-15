using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Model.UserClass;
using Model.UserInsertClass;
using Moq;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;
using Services;
using Shared.Interfaces.Models;
using SharedRepository;
using SharedServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Caching.Memory;
using Services.HelperServices;
using Services.Models;
using SharedServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.ContextFolder;
using Microsoft.Extensions.Logging;
using Castle.Core.Resource;
using Org.BouncyCastle.Crypto.Generators;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Hosting;

namespace Test.Fixture;

[CollectionDefinition(Name)]
public class TestCollection : ICollectionFixture<TestHostFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.

    public const string Name = "Test Collection";
}

public class TestHostFixture
{
    public readonly HttpClient httpClient;

    public TestHostFixture()
    {
        var webAppFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<DbContext>(options =>
                    {
                        // Remove the app's ApplicationDbContext registration.
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                typeof(DbContextOptions<Context>));

                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }
                        // Add a database context using an in-memory 
                        // database for testing.
                        services.AddDbContext<Context>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDbForTesting");
                        });
                        var sp = services.BuildServiceProvider();

                        using (var scope = sp.CreateScope())
                        {
                            var scopedServices = scope.ServiceProvider;
                            var context = scopedServices.GetRequiredService<Context>();

                            // Ensure the database is created.
                            context.Database.EnsureDeleted();
                            context.Database.EnsureCreated();

                            //try
                            //{
                            // Seed the database with test data.
                            //SeedSampleData(context);
                            var user = new User
                            {
                                Id = 1,
                                Name = "Andjela Filipovic",
                                Email = "andjela@gmail.com",
                                Password = "oRdR0yIrrbZFdjs/3mJJ6Qi1/5E=",
                                Salt = "1111"

                            };
                            context.Users.Add(user);


                            context.SaveChanges();


                            //}
                            //catch (Exception ex)
                            //{
                            //    throw ex;
                            //}
                        }
                    });
                });
            });
        httpClient = webAppFactory.CreateClient();

    }
    private void SeedSampleData(Context context)
    {
        var user = new User
        {
            Id = 1,
            Name = "Andjela Filipovic",
            Email = "andjela@gmail.com",
            Password = "oRdR0yIrrbZFdjs/3mJJ6Qi1/5E=",
            Salt = "1111"

        };
        context.Users.Add(user);
        context.SaveChanges();
    }
   

}



