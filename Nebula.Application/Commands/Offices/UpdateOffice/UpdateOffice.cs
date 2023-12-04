using Nebula.Domain.Entities.Offices;
using System.Data;

namespace Nebula.Application.Commands.Offices.UpdateOffice;

public record UpdateOfficeCommand : IRequest<Office>
{
    public UpdateOfficeCommand(long id, string name, string address, string city, string state, 
        string postalCode, string country, string phone, string email, string website, string description)
    {
        Id = id;
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

    public long Id { get; set; }
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


public class UpdateOfficeCommandHandler : IRequestHandler<UpdateOfficeCommand, Office>
{
    private readonly IRepository<Office> repository;

    public UpdateOfficeCommandHandler(IRepository<Office> repository)
    {
        this.repository = repository;
    }

    public async Task<Office> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
    {
        var office = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Office was not found!");

        office.Name = request.Name;
        office.Address = request.Address;
        office.City = request.City;
        office.State = request.State;
        office.PostalCode = request.PostalCode;
        office.Country = request.Country;
        office.Phone = request.Phone;
        office.Email = request.Email;
        office.Website = request.Website;
        office.Description = request.Description;

        repository.Update(office);
        await repository.SaveAsync();

        return office;
    }
}
