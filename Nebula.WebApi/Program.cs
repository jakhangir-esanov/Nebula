using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nebula.Application.Interfaces;
using Nebula.Infrastructure.Contexts;
using Nebula.Infrastructure.Repository;
using Nebula.Application.Commands.People.CreateCustomer;
using Nebula.Domain.Entities.People;
using Nebula.Application.Mappings;
using Nebula.Application.Queries.People.GetUser;
using Nebula.Application.DTOs;
using Nebula.Application.Commands.Offices.CreateOffice;
using Nebula.Domain.Entities.Offices;
using Nebula.Application.Commands.People.CreateUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//AppDbContext
builder.Services.AddDbContext<AppDbContext>(options
    => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

//MediatR
builder.Services.AddTransient<IRequestHandler<CreateCustomerCommand, Customer>, CreateCustomerCommandHandler>();
builder.Services.AddTransient<IRequestHandler<CreateUserCommand, User>, CreateUserCommandHandler>();
builder.Services.AddTransient<IRequestHandler<RetrieveByIdQuery, UserResultDto>, RetrieveByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<CreateOfficeCommand, Office>, CreateOfficeCommandHandler>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

//Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
