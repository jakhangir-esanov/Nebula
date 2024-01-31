namespace Nebula.Application.DTOs;

public class FileInformationDto
{
    public bool Exists { get; set; }
    public bool IsDirectory { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public long Length { get; set; }
    public string Name { get; set; }
    public string PhysicalPath { get; set; }
}

