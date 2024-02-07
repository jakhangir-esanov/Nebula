using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Queries.Cars.GetCar;

public record GetAllCarsQuery : IRequest<IEnumerable<CarResultDto>>
{
}

public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, IEnumerable<CarResultDto>>
{
    private readonly IRepository<Car> repository;
    private readonly IMapper mapper;
    public GetAllCarsQueryHandler(IRepository<Car> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<CarResultDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        var cars = this.repository.SelectAll(
            includes: new[] { "Attachments", "CarRentals" }).ToList();
        var result = mapper.Map<IEnumerable<CarResultDto>>(cars);

        foreach (var res in result)
        {
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
        }

        return Task.FromResult(result);
    }
}
