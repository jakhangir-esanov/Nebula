using Nebula.Application.DTOs;
using Nebula.Application.Extentions;
using Nebula.Application.Helpers;
using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Commands.Attachments.UpdateAttachment;

public record UpdateCarAttachmentCommand : IRequest<Attachment>
{
    public UpdateCarAttachmentCommand(string dirName, long attachmentId, long carId, AttachmentCreationDto dto)
    {
        this.dirName = dirName;
        this.attachmentId = attachmentId;
        this.carId = carId;
        this.dto = dto;
    }

    public string dirName { get; set; }
    public long attachmentId { get; set; }
    public long carId { get; set; }
    public AttachmentCreationDto dto { get; set; }
}

public class UpdateCarAttachmentCommandHandler : IRequestHandler<UpdateCarAttachmentCommand, Attachment>
{
    private readonly IRepository<Attachment> repository;

    public UpdateCarAttachmentCommandHandler(IRepository<Attachment> repository)
    {
        this.repository = repository;
    }

    public async Task<Attachment> Handle(UpdateCarAttachmentCommand request, CancellationToken cancellationToken)
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
        attachment.CarId = request.carId;

        this.repository.Update(attachment);
        await this.repository.SaveAsync();

        return attachment;
    }
}
