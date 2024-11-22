using RideFind_BackEnd.IAM;
using RideFind_BackEnd.IAM.Application.Internal.CommandServices;
using RideFind_BackEnd.IAM.Application.Internal.OutboundServices;
using RideFind_BackEnd.IAM.Application.Internal.QueryServices;
using RideFind_BackEnd.IAM.Infrastructure.Hashing.BCrypt.Services;
using RideFind_BackEnd.IAM.Infrastructure.Persistence.EFC.Repositories;
using RideFind_BackEnd.IAM.Infrastructure.Pipeline.Extensions;
using RideFind_BackEnd.IAM.Infrastructure.Tokens.JWT.Configuration;
using RideFind_BackEnd.IAM.Infrastructure.Tokens.JWT.Services;
using RideFind_BackEnd.IAM.Interfaces.ACL.Services;
using RideFind_BackEnd.IAM.Interfaces.ACL.Services.Services;
using RideFind_BackEnd.Profiles;
using RideFind_BackEnd.Shared.Domain.Repositories;
using RideFind_BackEnd.Shared.Infraestructure.Persistence.EFC.Repositories;
using RideFind_BackEnd.Shared.Interfaces.ASP.Configuration;
using RideFind_BackEnd.Vehicles.Application.Internal.CommandServices;
using RideFind_BackEnd.Vehicles.Application.Internal.QueryServices;
using RideFind_BackEnd.Vehicles.Domain.Repositories;
using RideFind_BackEnd.Vehicles.Domain.Services;
using RideFind_BackEnd.Vehicles.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure Database Context and Logging Levels

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if (connectionString != null)
            if (builder.Environment.IsDevelopment())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            else if (builder.Environment.IsProduction())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableDetailedErrors();
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "RideFind.Api",
                Version = "v1",
                Description = "RideFind Company API",
                TermsOfService = new Uri("https://RideFind.com/tos"),
                Contact = new OpenApiContact
                {
                    Name = "RideFind",
                    Email = "RideFind.support@ridefind.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
        c.EnableAnnotations();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                Array.Empty<string>()
            }
        });
    });

// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Configure Dependency Injection

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Publishing Bounded Context Injection Configuration
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryCommandServices, CategoryCommandService>();
builder.Services.AddScoped<ICategoryQueryServices, CategoryQueryService>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleCommandServices, VehicleCommandService>();
builder.Services.AddScoped<IVehicleQueryServices, VehicleQueryService>();

// Profiles Bounded Context Injection Configuration
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();
builder.Services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();

// IAM Bounded Context Injection Configuration

// TokenSettings Configuration

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryServices, UserQueryServices>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();


var app = builder.Build();

// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RideFind API V1");
        c.RoutePrefix = string.Empty; // Esto hace que Swagger esté disponible en la raíz
    });
}

app.UseCors("AllowAllPolicy");

// Add Authorization Middleware to Pipeline
app.UseRequestAuthorization();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();