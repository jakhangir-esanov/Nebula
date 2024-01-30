using Nebula.Application.DTOs;
using Nebula.Application.Extentions;
using Nebula.Application.Helpers;
using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Commands.Attachments.CreateAttachment;

public record CreateCarAttachmentCommand : IRequest<Attachment>
{
    public CreateCarAttachmentCommand(string dirName, long carId, AttachmentCreationDto dto)
    {
        this.dirName = dirName;
        this.carId = carId;
        this.dto = dto;
    }

    public string dirName { get; set; }
    public long carId { get; set; }
    public AttachmentCreationDto dto { get; set; }
}

public class CreateCarAttachmentCommandHandler : IRequestHandler<CreateCarAttachmentCommand, Attachment>
{
    private readonly IRepository<Attachment> repository;

    public CreateCarAttachmentCommandHandler(IRepository<Attachment> repository)
    {
        this.repository = repository;
    }

    public async Task<Attachment> Handle(CreateCarAttachmentCommand request, CancellationToken cancellationToken)
    {
        var webrootPath = Path.Combine(PathHelper.WebRootPath, request.dirName);

        if (!Directory.Exists(webrootPath))
            Directory.CreateDirectory(webrootPath);

        var fileExtension = Path.GetExtension(request.dto.FormFile.FileName);
        var fileName = $"{Guid.NewGuid().ToString("N")}{fileExtension}";
        var fullPath = Path.Combine(webrootPath, fileName);

        var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        await fileStream.WriteAsync(request.dto.FormFile.ToByte());

        var createdAttachment = new Attachment
        {
            FileName = fileName,
            FilePath = fullPath,
            CarId = request.carId
        };

        await this.repository.InsertAsync(createdAttachment);
        await this.repository.SaveAsync();

        return createdAttachment;
    }
}
