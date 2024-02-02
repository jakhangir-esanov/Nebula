﻿namespace Nebula.Application.DTOs;

public class CarResultDto
{
    public CarResultDto()
    {
        Attachments = new HashSet<AttachmentResultDto>();
        Files = new HashSet<FileInformationDto>();
    }
    public long Id { get; set; }
    public string Model { get; set; }
    public DateTime Year { get; set; }
    public string Color { get; set; }
    public string Number { get; set; }
    public double Price { get; set; }
    public long RegistrationNumber { get; set; }
    public bool IsAvailable { get; set; } = true;
    public long CarCategoryId { get; set; }
    public ICollection<AttachmentResultDto> Attachments { get; set; }
    public ICollection<FileInformationDto> Files { get; set; }
}
