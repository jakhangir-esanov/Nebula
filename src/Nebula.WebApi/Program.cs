using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nebula.Application.Helpers;
using Nebula.Infrastructure.Contexts;
using Nebula.WebApi.Extentions;
using Nebula.WebApi.Middlewares;
using Nebula.WebApi.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Extention for Swagger
builder.Services.ConfigureSwagger();

//AppDbContext
builder.Services.AddDbContext<AppDbContext>(options
    => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//PathRoot
PathHelper.WebRootPath = Path.GetFullPath("wwwroot");

//MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

//Logger
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//ServiceCollection
builder.Services.AddServices();

//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

//Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//Service
builder.Services.AddScoped<IAuthService, AuthService>();

//JWT
builder.Services.AddJwt(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
