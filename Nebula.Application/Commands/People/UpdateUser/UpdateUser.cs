using Nebula.Application.Interfaces;
using Nebula.Domain.Entities.People;
using Nebula.Domain.Enums;
using Nebula.Domain.Exceptions;

namespace Nebula.Application.Commands.People.UpdateUser;

public record UpdateUserCommand : IRequest<User>
{
    public UpdateUserCommand(long id, string firstName, string lastName, string username, string email,
        string phone, string password, DateTime dateOfBirth, UserRole userRole, long officeId)
    {
        Id = id;
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

    public long Id { get; set; }
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

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateUserCommand, User>
{
    private readonly IRepository<User> repository;

    public UpdateCustomerCommandHandler(IRepository<User> repository)
    {
        this.repository = repository;
    }

    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("User was not found!");

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.username = request.username;
        user.Email = request.Email;
        user.Phone = request.Phone;
        user.Password = request.Password;
        user.DateOfBirth = request.DateOfBirth;
        user.UserRole = request.UserRole;
        user.OfficeId = request.OfficeId;

        repository.Update(user);
        await repository.SaveAsync();

        return user;
    }
}

