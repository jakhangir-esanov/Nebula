using Nebula.Application.DTOs;
using Nebula.Application.Extentions;
using Nebula.Application.Helpers;
using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Commands.Attachments.CreateAttachment;

public record CreateAttachmentCommand : IRequest<Attachment>   
{
    public CreateAttachmentCommand(string dirName, AttachmentCreationDto dto)
    {
        this.dirName = dirName;
        this.dto = dto;
    }

    public string dirName { get; set; }
    public AttachmentCreationDto dto { get; set; }
}


public class CreateAttachmentCommandHandler : IRequestHandler<CreateAttachmentCommand, Attachment>
{
    private readonly IRepository<Attachment> repository;

    public CreateAttachmentCommandHandler(IRepository<Attachment> repository)
    {
        this.repository = repository;
    }

    public async Task<Attachment> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
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
            FilePath = fullPath
        };

        await this.repository.InsertAsync(createdAttachment);
        await this.repository.SaveAsync();

        return createdAttachment;
    }
}