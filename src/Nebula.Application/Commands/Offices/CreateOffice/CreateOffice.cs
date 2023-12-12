using Nebula.Domain.Entities.Offices;

namespace Nebula.Application.Commands.Offices.CreateOffice;

public record CreateOfficeCommand : IRequest<Office>
{
    public CreateOfficeCommand(string name, string address, string city, string state, string postalCode,
        string country, string phone, string email, string website, string description)
    {
        Name = name;
        Address = address;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
        Phone = phone;
        Email = email;
        Website = website;
        Description = description;
    }

    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public string Description { get; set; }
}

public class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, Office>
{
    private readonly IRepository<Office> repository;

    public CreateOfficeCommandHandler(IRepository<Office> repository)
    {
        this.repository = repository;
    }

    public async Task<Office> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
    {
        var office = await repository.SelectAsync(x => x.Email.Equals(request.Email));
        if (office is not null)
            throw new AlreadyExistException("Office is already exist!");

        var newOffice = new Office()
        {
            Name = request.Name,
            Address = request.Address,
            City = request.City,
            State = request.State,
            PostalCode = request.PostalCode,
            Country = request.Country,
            Phone = request.Phone,
            Email = request.Email,
            Website = request.Website,
            Description = request.Description
        };

        await repository.InsertAsync(newOffice);
        await repository.SaveAsync();

        return newOffice;
    }
}
