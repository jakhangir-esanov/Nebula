using Nebula.Application.Helpers;
using Nebula.Domain.Entities.People;

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
    private readonly IRepository<User> userRepository;

    public CreateCustomerCommandHandler(IRepository<Customer> repository, IRepository<User> userRepository)
    {
        this.repository = repository;
        this.userRepository = userRepository;
    }

    public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await repository.SelectAsync(x => x.Email.Equals(request.Email));
        if (customer is not null)
            throw new AlreadyExistException("Customer is already exist!");
        var user = await userRepository.SelectAsync(x => x.Email.Equals(request.Email));
        if (user is not null)
            throw new AlreadyExistException("Person is already exist, this is user!");

        var hasResult = PasswordHasher.Hash(request.Password);

        var newCustomer = new Customer()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Password = hasResult.Password,
            Salt = hasResult.Salt,
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