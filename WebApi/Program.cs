using DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services;
using Services.HelperServices;
using Services.Models;
using SharedServices;
using SharedServices.Interfaces;
using System.Text;

//var builder = WebApplication.CreateBuilder(new WebApplicationOptions
//{
//    Args = args,
//    ApplicationName = typeof(Program).Assembly.FullName,
//    ContentRootPath = Directory.GetCurrentDirectory(),
//    EnvironmentName = Environments.Staging,
//    WebRootPath = "webroot"
//});

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthentication(obj =>
{
    obj.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    obj.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    obj.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions =>
{
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new List<string>()
        }
    });
});


builder.Services.AddHostedService<HostedService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<IEmailHelperService, EmailHelperService>();

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddResponseCaching();
builder.Services.AddSingleton<DictionaryService>();
builder.Services.AddSingleton<MemoryCacheService>();

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<IUrlHelper>(factory =>
{
    var actionContext = factory.GetService<IActionContextAccessor>()
                                   .ActionContext;
    return new UrlHelper(actionContext);
});


IConfiguration config = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.AddEnvironmentVariables()
.Build();
var connectionString = config.GetSection("ConnectionString").Value;

builder.Services.StartDto();
builder.Services.SetRepository(connectionString, config);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test.API v1"));
//}

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.Run();
