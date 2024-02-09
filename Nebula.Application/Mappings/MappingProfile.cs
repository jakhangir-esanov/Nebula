using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Attachments;
using Nebula.Domain.Entities.Cars;
using Nebula.Domain.Entities.Feedbacks;
using Nebula.Domain.Entities.Insurances;
using Nebula.Domain.Entities.Offices;
using Nebula.Domain.Entities.Payments;
using Nebula.Domain.Entities.People;
using Nebula.Domain.Entities.Rentals;

namespace Nebula.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AttachmentResultDto, Attachment>().ReverseMap();
        CreateMap<CarCategory, CarCategoryResultDto>().ReverseMap();
        CreateMap<CarResultDto, Car>().ReverseMap();
        CreateMap<CustomerResultDto, Customer>().ReverseMap();
        CreateMap<FeedbackResultDto, Feedback>().ReverseMap();
        CreateMap<InsuranceCoverageResultDto, InsuranceCoverage>().ReverseMap();
        CreateMap<InsuranceResultDto, Insurance>().ReverseMap();
        CreateMap<OfficeAttachmentResultDto, OfficeAttachment>().ReverseMap();
        CreateMap<OfficeResultDto, Office>().ReverseMap();
        CreateMap<PaymentHistoryResultDto, PaymentHistory>().ReverseMap();
        CreateMap<PaymentResultDto, Payment>().ReverseMap();
        CreateMap<RentalResultDto, Rental>().ReverseMap();
        CreateMap<UserResultDto, User>().ReverseMap();
    }
}
