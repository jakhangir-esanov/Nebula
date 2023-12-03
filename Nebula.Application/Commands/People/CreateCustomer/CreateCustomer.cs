using Nebula.Application.Interfaces;
using Nebula.Domain.Entities.People;
using Nebula.Domain.Exceptions;

namespace Nebula.Application.Commands.People.CreateCustomer;

public record CreateCustomerCommand : IRequest<Customer>
{
    public CreateCustomerCommand(string firstName, string lastName, string email, string phone, string password, 
        DateTime dateOfBirth, string address, string drivingLicenseNumber, DateTime drivingLicenseExpirationDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Password = password;
        DateOfBirth = dateOfBirth;
        Address = address;
        DrivingLicenseNumber = drivingLicenseNumber;
        DrivingLicenseExpirationDate = drivingLicenseExpirationDate;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public string DrivingLicenseNumber { get; set; }
    public DateTime DrivingLicenseExpirationDate { get; set; }
}


public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
{
    private readonly IRepository<Customer> repository;

    public CreateCustomerCommandHandler(IRepository<Customer> repository)
    {
        this.repository = repository;
    }

    public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await repository.SelectAsync(x => x.Email.Equals(request.Email));
        if (customer is not null)
            throw new AlreadyExistException("Customer is already exist!");

        var newCustomer = new Customer()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Password = request.Password,
            DateOfBirth = request.DateOfBirth,
            Address = request.Address,
            DrivingLicenseExpirationDate = request.DrivingLicenseExpirationDate,
            DrivingLicenseNumber = request.DrivingLicenseNumber
        };

        await repository.InsertAsync(newCustomer);
        await repository.SaveAsync();

        return newCustomer;
    }
}