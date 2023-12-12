using Nebula.Application.DTOs;
using Nebula.Application.Extentions;
using Nebula.Application.Helpers;
using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Commands.Attachments.UpdateAttachment;

public record UpdateAttachmentCommand : IRequest<Attachment>
{
    public UpdateAttachmentCommand(string dirName, long attachmentId, AttachmentCreationDto dto)
    {
        this.dirName = dirName;
        this.attachmentId = attachmentId;
        this.dto = dto;
    }

    public string dirName {  get; set; }
    public long attachmentId { get; set; }
    public AttachmentCreationDto dto { get; set; }
}

public class UpdateAttachmentCommandHandler : IRequestHandler<UpdateAttachmentCommand, Attachment>
{
    private readonly IRepository<Attachment> repository;

    public UpdateAttachmentCommandHandler(IRepository<Attachment> repository)
    {
        this.repository = repository;
    }

    public async Task<Attachment> Handle(UpdateAttachmentCommand request, CancellationToken cancellationToken)
    {
        var attachment = await repository.SelectAsync(x => x.Id.Equals(request.attachmentId))
                   ?? throw new NotFoundException("Attachment was not found!");

        File.Delete(attachment.FilePath);

        var webrootPath = Path.Combine(PathHelper.WebRootPath, request.dirName);

        if (!Directory.Exists(webrootPath))
            Directory.CreateDirectory(webrootPath);

        var fileExtension = Path.GetExtension(request.dto.FormFile.FileName);
        var fileName = $"{Guid.NewGuid().ToString("N")}{fileExtension}";
        var fullPath = Path.Combine(webrootPath, fileName);

        var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        await fileStream.WriteAsync(request.dto.FormFile.ToByte());

        attachment.FileName = fileName;
        attachment.FilePath = fullPath;

        this.repository.Update(attachment);
        await this.repository.SaveAsync();

        return attachment;
    }
}
