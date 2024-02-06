using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Queries.Cars.GetCar;

public record GetCarQuery : IRequest<CarResultDto>
{
    public long Id { get; set; }
}

public class GetCarQueryHandler : IRequestHandler<GetCarQuery, CarResultDto>
{
    private readonly IRepository<Car> repository;
    private readonly IMapper mapper;
    public GetCarQueryHandler(IRepository<Car> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<CarResultDto> Handle(GetCarQuery request, CancellationToken cancellationToken)
    {
        var car = await this.repository.SelectAsync(x => x.Id.Equals(request.Id), 
            includes: new[] { "Attachments", "Rentals" })
            ?? throw new NotFoundException("Car was not found!");

        var res = mapper.Map<CarResultDto>(car);

        foreach (var item in res.Attachments) 
        {
            var file = new FileInformationDto()
            {
                Exists = System.IO.File.Exists(item.FilePath),
                IsDirectory = false,
                LastModified = System.IO.File.GetLastWriteTime(item.FilePath),
                Length = new FileInfo(item.FilePath).Length,
                Name = item.FileName,
                PhysicalPath = item.FilePath
            };

            res.Files.Add(file);
        }

        return res;
    }
}
