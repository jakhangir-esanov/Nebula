using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Attachments;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Queries.Cars.GetCar;

public record GetCarAttachmentQuery : IRequest<IEnumerable<FileInformationDto>>
{
    public long Id { get; set; }
}

public class GetCarAttachmentQueryHandler : IRequestHandler<GetCarAttachmentQuery, IEnumerable<FileInformationDto>>
{
    private readonly IRepository<Attachment> attachmentRepository;
    private readonly IRepository<Car> carRepository;
    public GetCarAttachmentQueryHandler(IRepository<Attachment> attachmentRepository, 
        IRepository<Car> carRepository)
    {
        this.attachmentRepository = attachmentRepository;
        this.carRepository = carRepository;
    }

    public async Task<IEnumerable<FileInformationDto>> Handle(GetCarAttachmentQuery request, CancellationToken cancellationToken)
    {
        var car = await this.carRepository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Car was not found!");

        var attachment = this.attachmentRepository.SelectAll(x => x.CarId.Equals(request.Id)).ToList();
        List<FileInformationDto> files = new List<FileInformationDto>();

        foreach (var file in attachment) 
        {
            var newFile = new FileInformationDto()
            {
                Exists = System.IO.File.Exists(file.FilePath),
                IsDirectory = false,
                LastModified = System.IO.File.GetLastWriteTime(file.FilePath),
                Length = new FileInfo(file.FilePath).Length,
                Name = file.FileName,
                PhysicalPath = file.FilePath
            };

            files.Add(newFile);
        }

        return files;
    }
}
