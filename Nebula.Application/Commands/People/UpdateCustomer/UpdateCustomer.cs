using Nebula.Application.Interfaces;
using Nebula.Domain.Entities.People;
using Nebula.Domain.Exceptions;

namespace Nebula.Application.Commands.People.UpdateCustomer;

public record UpdateCustomerCommand : IRequest<Customer>
{
    public UpdateCustomerCommand(long id, string firstName, string lastName, string email, string phone, 
        string password, DateTime dateOfBirth, string address, string drivingLicenseNumber, 
        DateTime drivingLicenseExpirationDate)
    {
        Id = id;
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

    public long Id { get; set; }
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

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
{
    private readonly IRepository<Customer> repository;

    public UpdateCustomerCommandHandler(IRepository<Customer> repository)
    {
        this.repository = repository;
    }

    public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Customer was not found!");

        customer.FirstName = request.FirstName;
        customer.LastName = request.LastName;
        customer.Email = request.Email;
        customer.Phone = request.Phone;
        customer.Password = request.Password;
        customer.DateOfBirth = request.DateOfBirth;
        customer.Address = request.Address;
        customer.DrivingLicenseNumber = request.DrivingLicenseNumber;
        customer.DrivingLicenseExpirationDate = request.DrivingLicenseExpirationDate;

        repository.Update(customer);
        await repository.SaveAsync();

        return customer;
    }
}

