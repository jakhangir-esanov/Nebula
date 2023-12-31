﻿using Nebula.Application.Helpers;
using Nebula.Domain.Entities.People;
using Nebula.Domain.Enums;

namespace Nebula.Application.Commands.People.CreateUser;

public record CreateUserCommand : IRequest<User>
{
    public CreateUserCommand(string firstName, string lastName, string username, string email,
        string phone, string password, DateTime dateOfBirth, UserRole userRole, long officeId)
    {
        FirstName = firstName;
        LastName = lastName;
        this.username = username;
        Email = email;
        Phone = phone;
        Password = password;
        DateOfBirth = dateOfBirth;
        UserRole = userRole;
        OfficeId = officeId;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string username { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public UserRole UserRole { get; set; }
    public long OfficeId { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IRepository<User> repository;
    private readonly IRepository<Customer> customerRepository;
    public CreateUserCommandHandler(IRepository<User> repository, IRepository<Customer> customerRepository)
    {
        this.repository = repository;
        this.customerRepository = customerRepository;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.SelectAsync(x => x.Email.Equals(request.Email));
        if (user is not null)
            throw new AlreadyExistException("Already exist!");
        var customer = await customerRepository.SelectAsync(x => x.Email.Equals(request.Email));
        if (customer is not null)
            throw new AlreadyExistException("Person is already exist, this is Customer!");

        var hasResult = PasswordHasher.Hash(request.Password);

        var newUser = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            username = request.username,
            Email = request.Email,
            Phone = request.Phone,
            Password = hasResult.Password,
            Salt = hasResult.Salt,
            DateOfBirth = request.DateOfBirth,
            UserRole = request.UserRole,
            OfficeId = request.OfficeId
        };

        await repository.InsertAsync(newUser);
        await repository.SaveAsync();

        return newUser;
    }
}
