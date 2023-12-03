namespace Nebula.WebApi;

public class CustomerCreationDto
{
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
