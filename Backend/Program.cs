using AutoMapper;
using Carwale;
using Carwale.API;
using Carwale.Domain;
using Carwale.Domain.Repositories.CarRepository;
using Carwale.Domain.Repositories.MakeRepository;
using Carwale.Domain.Repositories.ModelRepository;
using Carwale.Domain.Repositories.UserRepository;
using Carwale.Mapper;
using Carwale.Services;
using Carwale.Services.CarService;
using Carwale.Services.UserService;
using Carwale.UserIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors();

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
});

builder.Services.AddDbContext<CwDbContext>();
builder.Services.AddScoped<DbInitializer>();
builder.Services.AddScoped<AppSettings>();

builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IModelRepository, ModelRepository>();
builder.Services.AddScoped<IMakeRepository, MakeRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<CWUserIdentity>();
builder.Services.AddHttpContextAccessor();

// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["AppSettings:Jwt:Issuer"],
        ValidAudience = builder.Configuration["AppSettings:Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Jwt:Secret"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();


var app = builder.Build();
using (var serviceScope = app.Services.CreateScope())
{
	var services = serviceScope.ServiceProvider;
	var dbInitializer = services.GetRequiredService<DbInitializer>();
	dbInitializer.MigrateDbsAsync().Wait();
	dbInitializer.SeedDataAsync().Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(builder =>
       builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod()
    );

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware(typeof(ErrorHandlingMiddleware));

app.MapControllers();

app.Run();
