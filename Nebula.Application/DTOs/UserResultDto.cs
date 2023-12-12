using Nebula.Domain.Enums;

namespace Nebula.Application.DTOs;

public class UserResultDto
{
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
