using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nebula.Application.Interfaces;
using Nebula.Infrastructure.Contexts;
using Nebula.Infrastructure.Repository;
using Nebula.Application.Commands.People.CreateCustomer;
using Nebula.Domain.Entities.People;

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
