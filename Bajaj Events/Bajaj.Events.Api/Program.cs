using Asp.Versioning;
using Bajaj.Events.Api.JWT;
using Bajaj.Events.Api.Middlewares;
using Bajaj.Events.Dal;
using Bajaj.Events.Dal.Authentication;
using Bajaj.Events.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("bajaj-api-version"),
        new HeaderApiVersionReader("x-bajaj-api-version")
    );
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.WithOrigins("http://localhost:4200", "http://localhost:3000");
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Bajaj:TokenSecret"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddDbContext<BajajEventsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BajajEventsConStr"));
});

builder.Services.AddAutoMapper(Assembly.Load(new AssemblyName("Bajaj.Events.Api")));
builder.Services.AddTransient<ICommonRepository<Role>,CommonRepository<Role>>();
builder.Services.AddTransient<ICommonRepository<User>, CommonRepository<User>>();
builder.Services.AddTransient<ICommonRepository<Employee>, CommonRepository<Employee>>();
builder.Services.AddTransient<ICommonRepository<Event>, CommonRepository<Event>>();
builder.Services.AddTransient<ICommonRepository<EventRegistration>, CommonRepository<EventRegistration>>();
builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddTransient<ITokenManager, TokenManager>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Authentication Token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JsonWebToken",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[]{}
        }
    });
});


var app = builder.Build();

// Configure logging
var logger = app.Logger;

// Log a message to the console
logger.LogInformation("Application starting up...");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();



//app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.ConfigureExceptionMiddleware();

app.Run();